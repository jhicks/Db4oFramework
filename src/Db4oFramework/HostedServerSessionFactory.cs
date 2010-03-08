using Db4objects.Db4o;
using Db4objects.Db4o.CS;
using Db4objects.Db4o.CS.Config;

namespace Db4oFramework
{
    /// <summary>
    /// Used to host a server within this process
    /// </summary>
    public class HostedServerSessionFactory : SessionFactoryBase
    {
        public class Access
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        private IObjectServer _server;

        /// <summary>
        /// Allows configuration of the server and opening to the outside
        /// </summary>
        public HostedServerSessionFactory(ICurrentSessionContext currentSessionContext, IServerConfiguration config, string dbFileName, int port, params Access[] access) 
            : base(currentSessionContext)
        {
            _server = Db4oClientServer.OpenServer(config, dbFileName, port);

            foreach(var account in access)
            {
                _server.GrantAccess(account.Username,account.Password);
            }
        }

        /// <summary>
        /// Creates a hosted server that is only accessible in process
        /// </summary>
        public HostedServerSessionFactory(ICurrentSessionContext currentSessionContext, string dbFileName)
            : this(currentSessionContext, Db4oClientServer.NewServerConfiguration(), dbFileName,0)
        {
        }

        protected override IObjectContainer OpenClient()
        {
            return _server.OpenClient();
        }

        protected override void DoDisposal()
        {
            if(_server == null)
            {
                return;
            }

            _server.Close();
            _server.Dispose();
            _server = null;
        }
    }
}