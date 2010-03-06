using System.Collections.Generic;
using System.Web;

namespace Db4oFramework
{
    public class WebCurrentSessionContext : DictionaryBasedCurrentSessionContextBase
    {
        private const string Key = "Db4o.Util.WebCurrentSessionContext::CurrentSessionContextKey";

        protected override IDictionary<ISessionFactory, ISession> Context
        {
            get { return HttpContext.Current.Items[Key] as IDictionary<ISessionFactory, ISession>; }
            set { HttpContext.Current.Items[Key] = value; }
        }
    }
}