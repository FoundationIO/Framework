/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
using System;
using Framework.Infrastructure.Constants;
using Framework.Infrastructure.Interfaces.DbAccess;

namespace Framework.Data.DbAccess
{
    public class Transaction : ITransaction
    {
        private readonly IDBManager dbManager;
        private bool commitOrRollbackhandled = false;
        private bool disposed = false;

        public Transaction(IDBManager dbManager, DBTransactionIsolationLevel dBTransactionIsolationLevel = DBTransactionIsolationLevel.Unspecified)
        {
            this.dbManager = dbManager;
            CurrentTransactionCount = this.dbManager.BeginTransaction(dBTransactionIsolationLevel);
        }

        public int CurrentTransactionCount { get; protected set; } = 0;

        public int Complete()
        {
            if (commitOrRollbackhandled)
                throw new Exception("Commit of Rollback is already called");

            var result = dbManager.CommitTransaction();
            commitOrRollbackhandled = true;
            return result;
        }

        public int Rollback()
        {
            if (commitOrRollbackhandled)
                throw new Exception("Commit of Rollback is already called");

            var result = dbManager.RollbackTransaction();
            commitOrRollbackhandled = true;

            return result;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (!commitOrRollbackhandled)
                        Rollback();
                }

                disposed = true;
            }
        }
    }
}
