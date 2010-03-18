using System;
using System.Transactions;
using Db4objects.Db4o;

namespace Db4oFramework
{
    public class TransactionalResourceManager : IEnlistmentNotification
    {
        private readonly IObjectContainer _objectContainer;
        private readonly Func<bool> _closeOnDone;

        public TransactionalResourceManager(IObjectContainer objectContainer, Func<bool> closeOnDone)
        {
            _objectContainer = objectContainer;
            _closeOnDone = closeOnDone;
        }

        public void Prepare(PreparingEnlistment preparingEnlistment)
        {
            preparingEnlistment.Prepared();
        }

        public void Commit(Enlistment enlistment)
        {
            _objectContainer.Commit();
            if(_closeOnDone())
            {
                _objectContainer.Close();
                _objectContainer.Dispose();
            }
            enlistment.Done();
        }

        public void Rollback(Enlistment enlistment)
        {
            _objectContainer.Rollback();
            if(_closeOnDone())
            {
                _objectContainer.Close();
                _objectContainer.Dispose();
            }
            enlistment.Done();
        }

        public void InDoubt(Enlistment enlistment)
        {
            enlistment.Done();
        }
    }
}