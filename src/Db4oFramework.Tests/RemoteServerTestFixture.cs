using System.IO;
using System.Threading;
using Db4objects.Db4o;
using Db4objects.Db4o.CS;
using NUnit.Framework;

namespace Db4oFramework.Tests
{
    public abstract class RemoteServerTestFixture
    {
        protected ICurrentSessionContext CurrentSessionContext;
        protected ISessionFactory SessionFactory;
        protected IObjectServer Server;
        protected string Host = "localhost";
        protected int Port = 1699;
        protected string Username = "db4o";
        protected string Password = "db4o";

        private const string DbFileName = @".\RemoteServerTestDb.yap";

        [SetUp]
        public void SetupContext()
        {
            CurrentSessionContext = new ThreadStaticCurrentSessionContext();
            Server = Db4oClientServer.OpenServer(Db4oClientServer.NewServerConfiguration(), "RemoteServerTestDb.yap", Port);
            Server.GrantAccess(Username,Password);
            SessionFactory = new RemoteServerSessionFactory(CurrentSessionContext,Host, Port, Username, Password);
        }

        [TearDown]
        public void DestroyContext()
        {
            SessionFactory.Dispose();
            Server.Close();
            Server.Dispose();
            // we sleep here because sometimes the file isnt released
            Thread.Sleep(1000);
            DeleteFile();
        }

        private static void DeleteFile()
        {
            if (File.Exists(DbFileName))
            {
                File.Delete(DbFileName);
            }
        }
    }

    [TestFixture]
    public class when_opening_a_session_for_a_remote_server : RemoteServerTestFixture
    {
        [Test]
        public void it_should_return_a_session()
        {
            using (var db = SessionFactory.OpenSession())
            {
                Assert.That(db, Is.Not.Null);
            }
        }

        [Test]
        public void it_should_return_a_new_session()
        {
            using (var client1 = SessionFactory.OpenSession())
            using (var client2 = SessionFactory.OpenSession())
            {
                Assert.That(client1, Is.Not.SameAs(client2));
            }
        }
    }
}