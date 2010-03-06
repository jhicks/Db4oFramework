using System;
using Db4objects.Db4o;
using Db4objects.Db4o.CS;
using Db4objects.Db4o.CS.Config;

namespace Db4oFramework
{
    /// <summary>
    /// Used to access a remote server over TCP/IP
    /// </summary>
    public class RemoteServerSessionFactory : SessionFactoryBase
    {
        private readonly Func<IClientConfiguration> _config;
        private readonly string _host;
        private readonly int _port;
        private readonly string _username;
        private readonly string _password;

        public RemoteServerSessionFactory(ICurrentSessionContext currentSessionContext, string host, int port, string username, string password) 
            : this(currentSessionContext, Db4oClientServer.NewClientConfiguration,host,port,username,password)
        {
        }

        public RemoteServerSessionFactory(ICurrentSessionContext currentSessionContext, Func<IClientConfiguration> config, string host, int port, string username, string password)
            : base(currentSessionContext)
        {
            _config = config;
            _host = host;
            _port = port;
            _username = username;
            _password = password;
        }

        protected override IObjectContainer OpenClient()
        {
            return Db4oClientServer.OpenClient(_config(), _host, _port, _username, _password);
        }

        protected override void DoDisposal()
        {
        }
    }
}