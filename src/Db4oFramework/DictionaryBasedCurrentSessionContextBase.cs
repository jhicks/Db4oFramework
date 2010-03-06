using System.Collections.Generic;

namespace Db4oFramework
{
    public abstract class DictionaryBasedCurrentSessionContextBase : CurrentSessionContextBase
    {
        protected abstract IDictionary<ISessionFactory, ISession> Context { get; set; }

        protected override ISession DoGetSession(ISessionFactory sessionFactory)
        {
            return Context != null && Context.ContainsKey(sessionFactory) ? Context[sessionFactory] : null;
        }

        protected override void DoBind(ISession session)
        {
            Context = Context ?? new Dictionary<ISessionFactory, ISession>();
            Context.Add(session.SessionFactory,session);
        }

        protected override ISession DoUnbind(ISessionFactory sessionFactory)
        {
            ISession session = null;

            if (Context != null)
            {
                if (Context.ContainsKey(sessionFactory))
                {
                    session = Context[sessionFactory];
                    Context.Remove(sessionFactory);
                }

                if (Context.Count == 0)
                {
                    Context = null;
                }
            }

            return session;
        }
    }
}