using System.IO;
using NUnit.Framework;

namespace Db4oFramework.Tests
{
    public abstract class HostedServerTestFixture
    {
        protected const string DbFileName = ".\\HostedServerTestDb.yap";

        protected ISessionFactory SessionFactory;
        protected ICurrentSessionContext CurrentSessionContext;

        [SetUp]
        public void SetupContext()
        {
            CurrentSessionContext = new ThreadStaticCurrentSessionContext();
            SessionFactory = new HostedServerSessionFactory(CurrentSessionContext,DbFileName);
        }

        [TearDown]
        public void DestroyContext()
        {
            SessionFactory.Dispose();
            SessionFactory = null;
            DeleteFile();
        }

        private static void DeleteFile()
        {
            if(File.Exists(DbFileName))
            {
                File.Delete(DbFileName);
            }
        }
    }
    
    [TestFixture]
    public class when_opening_a_session_for_a_hosted_server : HostedServerTestFixture
    {
        [Test]
        public void it_should_return_a_session()
        {
            using(var client = SessionFactory.OpenSession())
            {
                Assert.That(client, Is.Not.Null);
            }
        }

        [Test]
        public void it_should_return_a_new_session()
        {
            using(var client1 = SessionFactory.OpenSession())
            using(var client2 = SessionFactory.OpenSession())
            {
                Assert.That(client1, Is.Not.SameAs(client2));
            }
        }
    }
}