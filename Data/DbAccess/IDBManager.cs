/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Framework.Infrastructure.Constants;
using LinqToDB;
using LinqToDB.Data;

namespace Framework.Data.DbAccess
{
    public interface IDBManager : IDisposable
    {
        string ConnectionString { get; set; }

        DataConnection Connection { get; }

        int BeginTransaction(DBTransactionIsolationLevel dBTransactionIsolationLevel);

        int CommitTransaction();

        int RollbackTransaction();

        ITable<T> GetTable<T>()
            where T : class;

        long Count<T>()
            where T : class;

        long Count<T>(Expression<Func<T, bool>> predicate)
            where T : class;

        int Delete<T>(Expression<Func<T, bool>> where)
            where T : class;

        T First<T>(Expression<Func<T, bool>> predicate)
            where T : class;

        T FirstOrDefault<T>(Expression<Func<T, bool>> predicate)
            where T : class;

        BulkCopyRowsCopied BulkCopy<T>(IEnumerable<T> lst)
            where T : class;

        void Insert<T>(IEnumerable<T> objs)
            where T : class;

        object Insert<T>(T obj)
            where T : class;

        void InsertAll<T>(List<T> list)
            where T : class;

        List<T> Select<T>(Expression<Func<T, bool>> predicate)
            where T : class;

        List<T> SelectAll<T>()
            where T : class;

        List<T> SelectByPage<T>(int pageNumber, int pageSize, Expression<Func<T, bool>> predicate = null)
            where T : class;

        List<T> SelectByPage<T>(int pageNumber, int pageSize, out long totalItems, Expression<Func<T, bool>> predicate = null)
            where T : class;

        void Update<T>(IEnumerable<T> objs)
            where T : class;

        void Update<T>(T obj)
            where T : class;
    }
}