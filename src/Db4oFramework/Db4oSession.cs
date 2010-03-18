using System;
using System.Collections;
using System.Collections.Generic;
using Db4objects.Db4o;
using Db4objects.Db4o.Ext;
using Db4objects.Db4o.Query;

namespace Db4oFramework
{
    public class Db4oSession : ISession
    {
        private readonly ISessionFactory _sessionFactory;
        private readonly IObjectContainer _objectContainer;
        private readonly TransactionWrapper _transactionWrapper;
 
        public Db4oSession(ISessionFactory sessionFactory, IObjectContainer objectContainer)
        {
            _sessionFactory = sessionFactory;
            _objectContainer = objectContainer;
            _transactionWrapper = new TransactionWrapper(objectContainer);
        }

        public void Dispose()
        {
            _transactionWrapper.Close();
        }

        public void Activate(object obj, int depth)
        {
            _objectContainer.Activate(obj,depth);
        }

        public bool Close()
        {
            _transactionWrapper.Close();
            return true;
        }

        public void Commit()
        {
            _transactionWrapper.Commit();
        }

        public void Deactivate(object obj, int depth)
        {
            _objectContainer.Deactivate(obj,depth);
        }

        public void Delete(object obj)
        {
            _objectContainer.Delete(obj);
        }

        public IExtObjectContainer Ext()
        {
            return _objectContainer.Ext();
        }

        [Obsolete("use QueryByExample")]
        public IObjectSet Get(object template)
        {
            return _objectContainer.Get(template);
        }

        public IObjectSet QueryByExample(object template)
        {
            return _objectContainer.QueryByExample(template);
        }

        IQuery IObjectContainer.Query()
        {
            return _objectContainer.Query();
        }

        public IObjectSet Query(Type clazz)
        {
            return _objectContainer.Query(clazz);
        }

        public IObjectSet Query(Predicate predicate)
        {
            return _objectContainer.Query(predicate);
        }

        public IObjectSet Query(Predicate predicate, IQueryComparator comparator)
        {
            return _objectContainer.Query(predicate,comparator);
        }

        public IObjectSet Query(Predicate predicate, IComparer comparer)
        {
            return _objectContainer.Query(predicate, comparer);
        }

        public void Rollback()
        {
            _transactionWrapper.Rollback();
        }

        [Obsolete("Use Store")]
        public void Set(object obj)
        {
            _objectContainer.Set(obj);
        }

        public void Store(object obj)
        {
            _objectContainer.Store(obj);
        }

        public IList<TExtent> Query<TExtent>(Predicate<TExtent> match)
        {
            return _objectContainer.Query(match);
        }

        public IList<TExtent> Query<TExtent>(Predicate<TExtent> match, IComparer<TExtent> comparer)
        {
            return _objectContainer.Query(match, comparer);
        }

        public IList<TExtent> Query<TExtent>(Predicate<TExtent> match, Comparison<TExtent> comparison)
        {
            return _objectContainer.Query(match, comparison);
        }

        public IList<TElementType> Query<TElementType>(Type extent)
        {
            return _objectContainer.Query<TElementType>(extent);
        }

        public IList<TExtent> Query<TExtent>()
        {
            return _objectContainer.Query<TExtent>();
        }

        public IList<TExtent> Query<TExtent>(IComparer<TExtent> comparer)
        {
            return _objectContainer.Query(comparer);
        }

        IQuery ISodaQueryFactory.Query()
        {
            return ((ISodaQueryFactory)_objectContainer).Query();
        }

        public ISessionFactory SessionFactory
        {
            get { return _sessionFactory; }
        }
    }
}