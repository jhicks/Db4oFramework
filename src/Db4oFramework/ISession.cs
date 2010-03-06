using Db4objects.Db4o;

namespace Db4oFramework
{
    public interface ISession : IObjectContainer
    {
        ISessionFactory SessionFactory { get; }        
    }
}