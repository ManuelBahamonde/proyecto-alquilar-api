
using Microsoft.EntityFrameworkCore.Storage;
using System;

namespace Alquilar.DAL
{
    public class BaseRepo
    {
        #region Members
        private IDbContextTransaction _tx;
        protected readonly DB _db;
        #endregion

        #region Constructor
        public BaseRepo(DB db)
        {
            _db = db;
        }
        #endregion

        public void BeginTransaction()
        {
            _tx = _db.Database.BeginTransaction();
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }

        public void Commit()
        {
            ValidateTransactionMethod();

            _tx.Commit();
            _tx = null;
        }
        public void Rollback()
        {
            ValidateTransactionMethod();

            _tx.Rollback();
            _tx = null;
        }

        private void ValidateTransactionMethod()
        {
            if (_tx == null)
                throw new InvalidOperationException("Ninguna transaccion fue iniciada.");
        }
    }
}
