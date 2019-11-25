/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Framework.Infrastructure.Constants;
using Framework.Infrastructure.Interfaces.DbAccess;

namespace Framework.Data.DbAccess
{
    public class Transaction : ITransaction
    {
        private readonly IDBManager dbManager;
        private readonly DBTransactionIsolationLevel dBTransactionIsolationLevel;
        private bool commitOrRollbackhandled = false;
        private bool isAsync = false;
        private bool disposed = false;

        public Transaction(IDBManager dbManager, DBTransactionIsolationLevel dBTransactionIsolationLevel = DBTransactionIsolationLevel.Unspecified)
        {
            this.dbManager = dbManager;
            this.dBTransactionIsolationLevel = dBTransactionIsolationLevel;
        }

        public int CurrentTransactionCount { get; protected set; } = 0;

        public int BeginTransaction()
        {
            CurrentTransactionCount = this.dbManager.BeginTransaction(dBTransactionIsolationLevel);
            return CurrentTransactionCount;
        }

        public async Task<int> BeginTransactionAsync()
        {
            CurrentTransactionCount = await this.dbManager.BeginTransactionAsync(dBTransactionIsolationLevel);
            return CurrentTransactionCount;
        }

        public int Complete()
        {
            isAsync = false;
            if (commitOrRollbackhandled)
                throw new Exception("Commit of Rollback is already called");

            var result = dbManager.CommitTransaction();
            commitOrRollbackhandled = true;
            return result;
        }

        public async Task<int> CompleteAsync()
        {
            isAsync = true;
            if (commitOrRollbackhandled)
                throw new Exception("Commit of Rollback is already called");

            var result = await dbManager.CommitTransactionAsync();
            commitOrRollbackhandled = true;
            return result;
        }

        public int Rollback()
        {
            isAsync = false;
            if (commitOrRollbackhandled)
                throw new Exception("Commit of Rollback is already called");

            var result = dbManager.RollbackTransaction();
            commitOrRollbackhandled = true;

            return result;
        }

        public async Task<int> RollbackAsync()
        {
            isAsync = true;
            if (commitOrRollbackhandled)
                throw new Exception("Commit of Rollback is already called");

            var result = await dbManager.RollbackTransactionAsync();
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
                    {
                        if (isAsync)
                            RollbackAsync().Wait();
                        else
                            Rollback();

                        if (Marshal.GetExceptionPointers() == IntPtr.Zero)
                        {
                            throw new Exception("Transaction is cancelled - Complete(Async) or Rollack(Async) is not called explicitly with in the transaction");
                        }
                    }
                }

                disposed = true;
            }
        }
    }
}
