using System;
using System.Collections.Generic;

namespace Db4oFramework
{
    public class ThreadStaticCurrentSessionContext : DictionaryBasedCurrentSessionContextBase
    {
        [ThreadStatic]
        private static IDictionary<ISessionFactory, ISession> _sessions;

        protected override IDictionary<ISessionFactory, ISession> Context
        {
            get { return _sessions; }
            set { _sessions = value; }
        }
    }
}