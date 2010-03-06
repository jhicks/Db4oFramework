using Db4objects.Db4o;
using NServiceBus;
using NServiceBus.Db4o;
using NUnit.Framework;
using Rhino.Mocks;

namespace NServicebus.Db4o.Tests
{
    [TestFixture]
    public class when_handling_begin_message
    {
        private IClientFactory _clientFactory;
        private IObjectContainer _client;
        private IMessageModule _mod;

        [SetUp]
        public void SetupContext()
        {
            _clientFactory = MockRepository.GenerateStrictMock<IClientFactory>();
            _client = MockRepository.GenerateStrictMock<IObjectContainer>();

            _clientFactory.Expect(x => x.OpenClient()).Return(_client);
            _clientFactory.Expect(x => x.Bind(_client));

            _mod = new Db4oMessageModule(_clientFactory);
        }

        [Test]
        public void it_should_open_a_new_client()
        {
            _mod.HandleBeginMessage();
            _clientFactory.AssertWasCalled(x => x.OpenClient());
        }

        [Test]
        public void it_should_bind_the_client_to_the_client_factory()
        {
            _mod.HandleBeginMessage();
            _clientFactory.AssertWasCalled(x => x.Bind(_client));
        }
    }

    [TestFixture]
    public class when_handling_end_message
    {
        private IClientFactory _clientFactory;
        private IObjectContainer _client;
        private IMessageModule _mod;

        [SetUp]
        public void SetupContext()
        {
            _clientFactory = MockRepository.GenerateStrictMock<IClientFactory>();
            _client = MockRepository.GenerateStrictMock<IObjectContainer>();

            _clientFactory.Expect(x => x.Unbind()).Return(_client);
            _client.Expect(x => x.Close()).Return(true);
            _client.Expect(x => x.Dispose());

            _mod = new Db4oMessageModule(_clientFactory);
        }

        [Test]
        public void it_should_unbind_the_client_and_close_it()
        {
            _mod.HandleEndMessage();
            _clientFactory.AssertWasCalled(x => x.Unbind());
        }

        [Test]
        public void it_should_close_the_client()
        {
            _mod.HandleEndMessage();
            _client.AssertWasCalled(x => x.Close());
        }

        [Test]
        public void it_should_dispose_the_client()
        {
            _mod.HandleEndMessage();
            _client.AssertWasCalled(x => x.Dispose());
        }
    }

    [TestFixture]
    public class when_handling_error_message
    {
        private IMessageModule _mod;
        private IClientFactory _clientFactory;
        private IObjectContainer _client;

        [SetUp]
        public void SetupContext()
        {
            _client = MockRepository.GenerateStrictMock<IObjectContainer>();
            _clientFactory = MockRepository.GenerateStrictMock<IClientFactory>();

            _clientFactory.Expect(x => x.Unbind()).Return(_client);
            _client.Expect(x => x.Rollback());
            _client.Expect(x => x.Close()).Return(true);
            _client.Expect(x => x.Dispose());

            _mod = new Db4oMessageModule(_clientFactory);
        }

        [Test]
        public void it_should_unbind_the_client()
        {
            _mod.HandleError();
            _clientFactory.AssertWasCalled(x => x.Unbind());
        }

        [Test]
        public void it_should_rollback_the_transaction()
        {
            _mod.HandleError();
            _client.AssertWasCalled(x => x.Rollback());
        }

        [Test]
        public void it_should_close_the_client()
        {
            _mod.HandleError();
            _client.AssertWasCalled(x => x.Close());
        }

        [Test]
        public void it_should_dispose_the_client()
        {
            _mod.HandleError();
            _client.AssertWasCalled(x => x.Dispose());
        }
    }
}