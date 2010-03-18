using System.Transactions;
using Db4objects.Db4o;

namespace Db4oFramework
{
    public class TransactionWrapper
    {
        private readonly IObjectContainer _objectContainer;
        private bool _closeOnTransactionComplete;

        public TransactionWrapper(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;

            if(Transaction.Current != null)
            {
                Transaction.Current.EnlistVolatile(new TransactionalResourceManager(objectContainer, () => _closeOnTransactionComplete), EnlistmentOptions.None);
            }
        }

        public void Commit()
        {
            if(Transaction.Current != null)
            {
                return;
            }

            _objectContainer.Commit();
        }

        public void Rollback()
        {
            if (Transaction.Current != null)
            {
                return;
            }

            _objectContainer.Rollback();
        }

        public void Close()
        {
            if (Transaction.Current != null)
            {
                _closeOnTransactionComplete = true;
                return;
            }

            _objectContainer.Close();
            _objectContainer.Dispose();
        }
    }
}