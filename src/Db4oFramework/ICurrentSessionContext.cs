namespace Db4oFramework
{
    public interface ICurrentSessionContext
    {
        ISession GetCurrentSession(ISessionFactory sessionFactory);
        void BindSession(ISession session);
        ISession UnbindSession(ISessionFactory sessionFactory);
        bool HasBoundSession(ISessionFactory sessionFactory);
    }
}