/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
using System.Threading.Tasks;
using Framework.Infrastructure.Constants;

namespace Framework.Infrastructure.Interfaces.DbAccess
{
    public interface ITransactionManager
    {
        ITransaction BeginTransaction(DBTransactionIsolationLevel dBTransactionIsolationLevel = DBTransactionIsolationLevel.Unspecified);

        Task<ITransaction> BeginTransactionAsync(DBTransactionIsolationLevel dBTransactionIsolationLevel = DBTransactionIsolationLevel.Unspecified);
    }
}
