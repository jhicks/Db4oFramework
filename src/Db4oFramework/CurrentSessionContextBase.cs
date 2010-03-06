using System;

namespace Db4oFramework
{
    public abstract class CurrentSessionContextBase : ICurrentSessionContext
    {
        protected abstract void DoBind(ISession session);
        protected abstract ISession DoUnbind(ISessionFactory sessionFactory);
        protected abstract ISession DoGetSession(ISessionFactory sessionFactory);

        public ISession GetCurrentSession(ISessionFactory sessionFactory)
        {
            var session = DoGetSession(sessionFactory);

            if(session == null)
            {
                throw new InvalidOperationException("No session bound to the current context.  Bind a session before calling GetCurrentSession");
            }

            return session;
        }

        public void BindSession(ISession session)
        {
            if(DoGetSession(session.SessionFactory) != null)
            {
                throw new InvalidOperationException("Orphaned session detected for the session context");
            }

            DoBind(session);
        }

        public ISession UnbindSession(ISessionFactory sessionFactory)
        {
            return DoUnbind(sessionFactory);
        }

        public bool HasBoundSession(ISessionFactory sessionFactory)
        {
            return DoGetSession(sessionFactory) != null;
        }
    }
}