using Db4objects.Db4o;

namespace Db4oFramework
{
    public abstract class SessionFactoryBase : ISessionFactory
    {
        private readonly ICurrentSessionContext _currentSessionContext;

        protected SessionFactoryBase(ICurrentSessionContext currentSessionContext)
        {
            _currentSessionContext = currentSessionContext;
        }

        private bool _disposed;

        public void Dispose()
        {
            if(_disposed)
            {
                return;
            }

            DoDisposal();
            _disposed = true;
        }

        protected virtual void DoDisposal() {}

        protected abstract IObjectContainer OpenClient();

        public ISession OpenSession()
        {
            return new Db4oSession(this, OpenClient());
        }

        public ISession GetCurrentSession()
        {
            return _currentSessionContext.GetCurrentSession(this);
        }

        public bool TryGetCurrentSession(out ISession session)
        {
            if(!_currentSessionContext.HasBoundSession(this))
            {
                session = null;
                return false;
            }

            session = _currentSessionContext.GetCurrentSession(this);
            return true;
        }

        public void Bind(ISession session)
        {
            _currentSessionContext.BindSession(session);
        }

        public ISession Unbind()
        {
            return _currentSessionContext.UnbindSession(this);
        }

        public bool HasBoundSession()
        {
            return _currentSessionContext.HasBoundSession(this);
        }
    }
}