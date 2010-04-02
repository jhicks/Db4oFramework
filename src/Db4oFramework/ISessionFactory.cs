using System;

namespace Db4oFramework
{
    public interface ISessionFactory : IDisposable
    {
        ISession OpenSession();
        ISession GetCurrentSession();        bool TryGetCurrentSession(out ISession session);
        void Bind(ISession session);
        ISession Unbind();
        bool HasBoundSession();
    }
}