/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
using System.Threading.Tasks;
using Framework.Infrastructure.Constants;
using Framework.Infrastructure.Interfaces.DbAccess;

namespace Framework.Data.DbAccess
{
    public class TransactionManager : ITransactionManager
    {
        private readonly IDBManager dbManager;

        public TransactionManager(IDBManager dbManager)
        {
            this.dbManager = dbManager;
        }

        public ITransaction BeginTransaction(DBTransactionIsolationLevel dBTransactionIsolationLevel = DBTransactionIsolationLevel.Unspecified)
        {
            var trx = new Transaction(dbManager, dBTransactionIsolationLevel);
            trx.BeginTransaction();
            return trx;
        }

        public async Task<ITransaction> BeginTransactionAsync(DBTransactionIsolationLevel dBTransactionIsolationLevel = DBTransactionIsolationLevel.Unspecified)
        {
            var trx = new Transaction(dbManager, dBTransactionIsolationLevel);
            await trx.BeginTransactionAsync();
            return trx;
        }
    }
}
