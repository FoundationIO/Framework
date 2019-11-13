/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Framework.Infrastructure.Constants;
using Framework.Infrastructure.Models.Result;
using LinqToDB;
using LinqToDB.Data;

namespace Framework.Data.DbAccess
{
    public interface IDBManager : IDisposable
    {
        string ConnectionString { get; set; }

        DataConnection Connection { get; }

        ITable<T> GetTable<T>(Expression<Func<T, bool>> predicate = null)
            where T : class;

        IQueryable<T> GetTableAsQueryable<T>(Expression<Func<T, bool>> predicate = null)
            where T : class;

        int BeginTransaction(DBTransactionIsolationLevel dBTransactionIsolationLevel);

        int CommitTransaction();

        int RollbackTransaction();

        bool Exists<T>()
            where T : class;

        bool Exists<T>(Expression<Func<T, bool>> predicate)
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

        void InsertWithAudit<T>(IEnumerable<T> objs, string createdBy)
            where T : class;

        object InsertWithAudit<T>(T obj, string createdBy)
            where T : class;

        void InsertAll<T>(List<T> list)
            where T : class;

        List<T> Select<T>(Expression<Func<T, bool>> predicate)
            where T : class;

        List<T> SelectAll<T>()
            where T : class;

        List<T> SelectByPage<T>(int pageNumber, int pageSize, Expression<Func<T, bool>> predicate = null)
            where T : class;

        DbReturnListModel<T> SelectByPageWithTotalRows<T>(int pageNumber, int pageSize, Expression<Func<T, bool>> predicate = null)
            where T : class;

        void Update<T>(IEnumerable<T> objs)
            where T : class;

        void Update<T>(T obj)
            where T : class;

        void UpdateWithAudit<T>(IEnumerable<T> objs, string modifiedBy)
            where T : class;

        void UpdateWithAudit<T>(T obj, string modifiedBy)
            where T : class;

        //Async
        Task<int> BeginTransactionAsync(DBTransactionIsolationLevel dBTransactionIsolationLevel);

        Task<int> CommitTransactionAsync();

        Task<int> RollbackTransactionAsync();

        Task<bool> ExistsAsync<T>()
            where T : class;

        Task<bool> ExistsAsync<T>(Expression<Func<T, bool>> predicate)
            where T : class;

        Task<long> CountAsync<T>()
            where T : class;

        Task<long> CountAsync<T>(Expression<Func<T, bool>> predicate)
            where T : class;

        Task<int> DeleteAsync<T>(Expression<Func<T, bool>> where)
            where T : class;

        Task<T> FirstAsync<T>(Expression<Func<T, bool>> predicate)
            where T : class;

        Task<T> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> predicate)
            where T : class;

        Task InsertAsync<T>(IEnumerable<T> objs)
            where T : class;

        Task<object> InsertAsync<T>(T obj)
            where T : class;

        Task InsertWithAuditAsync<T>(IEnumerable<T> objs, string createdBy)
            where T : class;

        Task<object> InsertWithAuditAsync<T>(T obj, string createdBy)
            where T : class;

        Task InsertAllAsync<T>(List<T> list)
            where T : class;

        Task<List<T>> SelectAsync<T>(Expression<Func<T, bool>> predicate)
            where T : class;

        Task<List<T>> SelectAllAsync<T>()
            where T : class;

        Task<List<T>> SelectByPageAsync<T>(int pageNumber, int pageSize, Expression<Func<T, bool>> predicate = null)
            where T : class;

        Task<DbReturnListModel<T>> SelectByPageWithTotalRowsAsync<T>(int pageNumber, int pageSize, Expression<Func<T, bool>> predicate = null)
            where T : class;

        Task UpdateAsync<T>(IEnumerable<T> objs)
            where T : class;

        Task UpdateAsync<T>(T obj)
            where T : class;

        Task UpdateWithAuditAsync<T>(IEnumerable<T> objs, string modifiedBy)
            where T : class;

        Task UpdateWithAuditAsync<T>(T obj, string modifiedBy)
            where T : class;
    }
}