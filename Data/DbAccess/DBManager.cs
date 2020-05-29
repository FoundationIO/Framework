/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Framework.Infrastructure.Config;
using Framework.Infrastructure.Constants;
using Framework.Infrastructure.Interfaces.DbAccess;
using Framework.Infrastructure.Logging;
using Framework.Infrastructure.Models.Config;
using Framework.Infrastructure.Models.Result;
using LinqToDB;
using LinqToDB.Data;

namespace Framework.Data.DbAccess
{
    public abstract class DBManager : IDBManager
    {
        private readonly LogSettings logConfig;
        private readonly ILog log;
        private readonly IDBInfo dbInfo;
        private DataConnection connection = null;
        private DataConnectionTransaction commonTransaction = null;
        private int currentTransactionCount = 0;
        private bool disposed = false;

        protected DBManager(IBaseConfiguration config, ILog log, IDBInfo dbInfo)
        {
            this.logConfig = config.LogSettings;
            this.log = log;
            this.dbInfo = dbInfo;
            ConnectionString = dbInfo.GetConnectionString();
            EnsureOpenConnection();
            ToggleLogging();
            log.Debug($"Creating Instance of {this.GetType()} with HashCode - {this.GetHashCode()}");
        }

        public string ConnectionString { get; set; }

        public LinqToDB.Data.DataConnection Connection
        {
            get
            {
                EnsureOpenConnection();
                return connection;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public ITable<T> GetTable<T>(Expression<Func<T, bool>> predicate = null)
            where T : class
        {
            if (predicate == null)
                return Connection.GetTable<T>();
            else
                return Connection.GetTable<T>().Where(predicate) as ITable<T>;
        }

        public IQueryable<T> GetTableAsQueryable<T>(Expression<Func<T, bool>> predicate = null)
            where T : class
        {
            if (predicate != null)
                return GetTable<T>().AsQueryable().Where(predicate);
            else
                return GetTable<T>().AsQueryable();
        }

        public int BeginTransaction(DBTransactionIsolationLevel dBTransactionIsolationLevel)
        {
            if (currentTransactionCount == 0)
                commonTransaction = Connection.BeginTransaction((IsolationLevel)dBTransactionIsolationLevel);

            currentTransactionCount++;
            log.SqlBeginTransaction(currentTransactionCount, currentTransactionCount == 0);
            return currentTransactionCount;
        }

        public int RollbackTransaction()
        {
            if (currentTransactionCount == 0)
                throw new Exception("Begin Transaction should be called before Rollback Transaction.");

            currentTransactionCount--;
            log.SqlRollbackTransaction(currentTransactionCount, currentTransactionCount == 0);
            if (currentTransactionCount == 0)
            {
                commonTransaction.Rollback();
            }

            return currentTransactionCount;
        }

        public int CommitTransaction()
        {
            if (currentTransactionCount == 0)
                throw new Exception("Begin Transaction should be called before Commit Transaction.");

            currentTransactionCount--;
            log.SqlCommitTransaction(currentTransactionCount, currentTransactionCount == 0);
            if (currentTransactionCount == 0)
            {
                commonTransaction.Commit();
                commonTransaction.Dispose();
                commonTransaction = null;
            }

            return currentTransactionCount;
        }

        public ITransaction BeginTransactionWithTransactionManager(DBTransactionIsolationLevel dBTransactionIsolationLevel = DBTransactionIsolationLevel.Unspecified)
        {
            var trx = new Transaction(this, dBTransactionIsolationLevel);
            trx.BeginTransaction();
            return trx;
        }

        public async Task<ITransaction> BeginTransactionWithTransactionManagerAsync(DBTransactionIsolationLevel dBTransactionIsolationLevel = DBTransactionIsolationLevel.Unspecified)
        {
            var trx = new Transaction(this, dBTransactionIsolationLevel);
            await trx.BeginTransactionAsync();
            return trx;
        }

        public int Delete<T>(Expression<Func<T, bool>> where)
            where T : class
        {
            return GetTable<T>().Delete(where);
        }

        public object Insert<T>(T obj)
            where T : class
        {
            return this.Connection.InsertWithIdentity<T>(obj);
        }

        public TKey Insert<T, TKey>(T obj)
            where T : class
            where TKey : struct
        {
            return (TKey)Convert.ChangeType(Insert(obj), typeof(TKey));
        }

        public void Insert<T>(IEnumerable<T> objs)
            where T : class
        {
            foreach (var obj in objs)
            {
                Insert(obj);
            }
        }

        public object InsertWithAudit<T>(T obj, string createdBy)
            where T : class
        {
            if (obj is IAuditableNameModel)
            {
                var aModel = obj as IAuditableNameModel;
                aModel.CreatedBy = createdBy;
                aModel.CreatedDate = DateTime.UtcNow;
                aModel.ModifiedBy = createdBy;
                aModel.ModifiedDate = aModel.CreatedDate;
            }

            return this.Connection.InsertWithIdentity<T>(obj);
        }

        public TKey InsertWithAudit<T, TKey>(T obj, string createdBy)
            where T : class
            where TKey : struct
        {
            return (TKey)Convert.ChangeType(InsertWithAudit(obj, createdBy), typeof(TKey));
        }

        public object InsertWithAudit<T>(T obj, long? createdBy)
            where T : class
        {
            if (obj is IAuditableIdModel)
            {
                var aModel = obj as IAuditableIdModel;
                aModel.CreatedBy = createdBy;
                aModel.CreatedDate = DateTime.UtcNow;
                aModel.ModifiedBy = createdBy;
                aModel.ModifiedDate = aModel.CreatedDate;
            }

            return this.Connection.InsertWithIdentity<T>(obj);
        }

        public TKey InsertWithAudit<T, TKey>(T obj, long? createdBy)
            where T : class
            where TKey : struct
        {
            return (TKey)Convert.ChangeType(InsertWithAudit(obj, createdBy), typeof(TKey));
        }

        public void InsertWithAudit<T>(IEnumerable<T> objs, string createdBy)
            where T : class
        {
            foreach (var obj in objs)
            {
                InsertWithAudit(obj, createdBy);
            }
        }

        public void InsertWithAudit<T>(IEnumerable<T> objs, long? createdBy)
            where T : class
        {
            foreach (var obj in objs)
            {
                InsertWithAudit(obj, createdBy);
            }
        }

        public bool Update<T>(T obj)
            where T : class
        {
            return Connection.Update<T>(obj) > 0;
        }

        public bool Update<T>(IEnumerable<T> objs)
            where T : class
        {
            var updated = true;
            foreach (var obj in objs)
            {
                updated = updated && Update(obj);
            }

            return updated;
        }

        public bool UpdateWithAudit<T>(T obj, string modifiedBy)
            where T : class
        {
            if (obj is IAuditableNameModel)
            {
                var aModel = obj as IAuditableNameModel;
                aModel.ModifiedDate = aModel.CreatedDate;
                aModel.ModifiedBy = modifiedBy;
            }

            return Connection.Update<T>(obj) > 0;
        }

        public bool UpdateWithAudit<T>(T obj, long? modifiedBy)
            where T : class
        {
            if (obj is IAuditableIdModel)
            {
                var aModel = obj as IAuditableIdModel;
                aModel.ModifiedDate = aModel.CreatedDate;
                aModel.ModifiedBy = modifiedBy;
            }

            return Connection.Update<T>(obj) > 0;
        }

        public bool UpdateWithAudit<T>(IEnumerable<T> objs, string modifiedBy)
            where T : class
        {
            var updated = true;
            foreach (var obj in objs)
            {
                updated = updated && UpdateWithAudit(obj, modifiedBy);
            }

            return updated;
        }

        public bool UpdateWithAudit<T>(IEnumerable<T> objs, long? modifiedBy)
            where T : class
        {
            var updated = true;
            foreach (var obj in objs)
            {
                updated = updated && UpdateWithAudit(obj, modifiedBy);
            }

            return updated;
        }

        public T First<T>(Expression<Func<T, bool>> predicate)
            where T : class
        {
            return GetTable<T>().First(predicate);
        }

        public T FirstOrDefault<T>(Expression<Func<T, bool>> predicate)
            where T : class
        {
            return GetTable<T>().FirstOrDefault(predicate);
        }

        public BulkCopyRowsCopied BulkCopy<T>(IEnumerable<T> lst)
            where T : class
        {
            return this.Connection.BulkCopy<T>(lst);
        }

        public List<T> Select<T>(Expression<Func<T, bool>> predicate)
            where T : class
        {
            var sql = GetTable<T>().Where(predicate);
            return sql.ToList();
        }

        public List<T> SelectAll<T>()
            where T : class
        {
            return GetTable<T>().ToList<T>();
        }

        public bool Exists<T>()
            where T : class
        {
            return GetTable<T>().Any();
        }

        public bool Exists<T>(Expression<Func<T, bool>> predicate)
            where T : class
        {
            return GetTable<T>().Any(predicate);
        }

        public long Count<T>(Expression<Func<T, bool>> predicate)
            where T : class
        {
            return GetTable<T>().Count(predicate);
        }

        public long Count<T>()
            where T : class
        {
            return GetTable<T>().Count<T>();
        }

        public DbReturnListModel<T> SelectByPageWithTotalRows<T>(int pageNumber, int pageSize, Expression<Func<T, bool>> predicate = null)
            where T : class
        {
            if (pageSize == 0)
                pageSize = 100;
            if (pageNumber <= 0)
                pageNumber = 1;

            var querable = GetTable<T>().Skip(pageSize * (pageNumber - 1)).Take(pageSize);
            long totalItems;
            if (predicate != null)
            {
                querable = querable.Where(predicate);
                totalItems = GetTable<T>().LongCount(predicate);
            }
            else
            {
                totalItems = GetTable<T>().LongCount();
            }

            return new DbReturnListModel<T>(querable.ToList(), totalItems);
        }

        public List<T> SelectByPage<T>(int pageNumber, int pageSize, Expression<Func<T, bool>> predicate = null)
            where T : class
        {
            if (pageSize == 0)
                pageSize = 100;

            if (pageNumber <= 0)
                pageNumber = 1;

            var querable = GetTable<T>().Skip(pageSize * (pageNumber - 1)).Take(pageSize);

            if (predicate != null)
            {
                querable = querable.Where(predicate);
            }

            return querable.ToList();
        }

        public List<object> InsertAll<T>(List<T> list)
            where T : class
        {
            var lst = new List<object>();
            foreach (var item in list)
            {
                lst.Add(Insert(item));
            }

            return lst;
        }

        public List<TKey> InsertAll<T, TKey>(List<T> list)
            where T : class
            where TKey : struct
        {
            var lst = new List<TKey>();
            foreach (var itemKey in InsertAll<T>(list))
            {
                lst.Add((TKey)itemKey);
            }

            return lst;
        }

        //Async Methods
        public async Task<int> BeginTransactionAsync(DBTransactionIsolationLevel dBTransactionIsolationLevel)
        {
            if (currentTransactionCount == 0)
                commonTransaction = await Connection.BeginTransactionAsync((IsolationLevel)dBTransactionIsolationLevel);

            currentTransactionCount++;
            log.SqlBeginTransaction(currentTransactionCount, currentTransactionCount == 0);
            return currentTransactionCount;
        }

        public async Task<int> RollbackTransactionAsync()
        {
            if (currentTransactionCount == 0)
                throw new Exception("Begin Transaction should be called before Rollback Transaction.");

            currentTransactionCount--;
            log.SqlRollbackTransaction(currentTransactionCount, currentTransactionCount == 0);
            if (currentTransactionCount == 0)
            {
                await commonTransaction.RollbackAsync();
            }

            return currentTransactionCount;
        }

        public async Task<int> CommitTransactionAsync()
        {
            if (currentTransactionCount == 0)
                throw new Exception("Begin Transaction should be called before Commit Transaction.");

            currentTransactionCount--;
            log.SqlCommitTransaction(currentTransactionCount, currentTransactionCount == 0);
            if (currentTransactionCount == 0)
            {
                await commonTransaction.CommitAsync();
                commonTransaction.Dispose();
                commonTransaction = null;
            }

            return currentTransactionCount;
        }

        public Task<int> DeleteAsync<T>(Expression<Func<T, bool>> where)
            where T : class
        {
            return GetTable<T>().DeleteAsync(where);
        }

        public Task<object> InsertAsync<T>(T obj)
            where T : class
        {
            return this.Connection.InsertWithIdentityAsync<T>(obj);
        }

        public async Task<TKey> InsertAsync<T, TKey>(T obj)
            where T : class
            where TKey : struct
        {
            return (TKey)Convert.ChangeType(await this.Connection.InsertWithIdentityAsync<T>(obj), typeof(TKey));
        }

        public async Task<List<object>> InsertAsync<T>(IEnumerable<T> objs)
            where T : class
        {
            var lst = new List<object>();
            foreach (var obj in objs)
            {
                lst.Add(await InsertAsync(obj));
            }

            return lst;
        }

        public async Task<List<TKey>> InsertAsync<T, TKey>(IEnumerable<T> objs)
            where T : class
            where TKey : struct
        {
            var lst = new List<TKey>();
            foreach (var objkey in await InsertAsync<T>(objs))
            {
                lst.Add((TKey)Convert.ChangeType(objkey, typeof(TKey)));
            }

            return lst;
        }

        public Task<object> InsertWithAuditAsync<T>(T obj, string createdBy)
            where T : class
        {
            if (obj is IAuditableNameModel)
            {
                var aModel = obj as IAuditableNameModel;
                aModel.CreatedBy = createdBy;
                aModel.CreatedDate = DateTime.UtcNow;
                aModel.ModifiedBy = createdBy;
                aModel.ModifiedDate = aModel.CreatedDate;
            }

            return this.Connection.InsertWithIdentityAsync<T>(obj);
        }

        public async Task<TKey> InsertWithAuditAsync<T, TKey>(T obj, string createdBy)
            where T : class
            where TKey : struct
        {
            return (TKey)Convert.ChangeType(await InsertWithAuditAsync<T>(obj, createdBy), typeof(TKey));
        }

        public Task<object> InsertWithAuditAsync<T>(T obj, long? createdBy)
            where T : class
        {
            if (obj is IAuditableIdModel)
            {
                var aModel = obj as IAuditableIdModel;
                aModel.CreatedBy = createdBy;
                aModel.CreatedDate = DateTime.UtcNow;
                aModel.ModifiedBy = createdBy;
                aModel.ModifiedDate = aModel.CreatedDate;
            }

            return this.Connection.InsertWithIdentityAsync<T>(obj);
        }

        public async Task<TKey> InsertWithAuditAsync<T, TKey>(T obj, long? createdBy)
            where T : class
            where TKey : struct
        {
            return (TKey)Convert.ChangeType(await InsertWithAuditAsync<T>(obj, createdBy), typeof(TKey));
        }

        public async Task<List<object>> InsertWithAuditAsync<T>(IEnumerable<T> objs, string createdBy)
            where T : class
        {
            var lst = new List<object>();
            foreach (var obj in objs)
            {
                lst.Add(await InsertWithAuditAsync(obj, createdBy));
            }

            return lst;
        }

        public async Task<List<TKey>> InsertWithAuditAsync<T, TKey>(IEnumerable<T> objs, string createdBy)
            where T : class
            where TKey : struct
        {
            var lst = new List<TKey>();
            foreach (var obj in objs)
            {
                lst.Add(await InsertWithAuditAsync<T, TKey>(obj, createdBy));
            }

            return lst;
        }

        public async Task<List<object>> InsertWithAuditAsync<T>(IEnumerable<T> objs, long? createdBy)
            where T : class
        {
            var lst = new List<object>();

            foreach (var obj in objs)
            {
                lst.Add(await InsertWithAuditAsync(obj, createdBy));
            }

            return lst;
        }

        public async Task<List<TKey>> InsertWithAuditAsync<T, TKey>(IEnumerable<T> objs, long? createdBy)
           where T : class
           where TKey : struct
        {
            var lst = new List<TKey>();
            foreach (var obj in objs)
            {
                lst.Add(await InsertWithAuditAsync<T, TKey>(obj, createdBy));
            }

            return lst;
        }

        public async Task<bool> UpdateAsync<T>(T obj)
            where T : class
        {
            return await Connection.UpdateAsync<T>(obj) > 0;
        }

        public async Task<bool> UpdateAsync<T>(IEnumerable<T> objs)
            where T : class
        {
            var updated = true;
            foreach (var obj in objs)
            {
                updated = updated && await UpdateAsync(obj);
            }

            return updated;
        }

        public async Task<bool> UpdateWithAuditAsync<T>(T obj, string modifiedBy)
            where T : class
        {
            if (obj is IAuditableNameModel)
            {
                var aModel = obj as IAuditableNameModel;
                aModel.ModifiedDate = aModel.CreatedDate;
                aModel.ModifiedBy = modifiedBy;
            }

            return await Connection.UpdateAsync<T>(obj) > 0;
        }

        public async Task<bool> UpdateWithAuditAsync<T>(T obj, long? modifiedBy)
            where T : class
        {
            if (obj is IAuditableIdModel)
            {
                var aModel = obj as IAuditableIdModel;
                aModel.ModifiedDate = aModel.CreatedDate;
                aModel.ModifiedBy = modifiedBy;
            }

            return await Connection.UpdateAsync<T>(obj) > 0;
        }

        public async Task<bool> UpdateWithAuditAsync<T>(IEnumerable<T> objs, string modifiedBy)
            where T : class
        {
            var updated = true;
            foreach (var obj in objs)
            {
                updated = updated && await UpdateWithAuditAsync(obj, modifiedBy);
            }

            return updated;
        }

        public async Task<bool> UpdateWithAuditAsync<T>(IEnumerable<T> objs, long? modifiedBy)
            where T : class
        {
            var updated = true;
            foreach (var obj in objs)
            {
                updated = updated && await UpdateWithAuditAsync(obj, modifiedBy);
            }

            return updated;
        }

        public Task<T> FirstAsync<T>(Expression<Func<T, bool>> predicate)
            where T : class
        {
            return GetTable<T>().FirstAsync(predicate);
        }

        public Task<T> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> predicate)
            where T : class
        {
            return GetTable<T>().FirstOrDefaultAsync(predicate);
        }

        public Task<List<T>> SelectAsync<T>(Expression<Func<T, bool>> predicate)
            where T : class
        {
            var sql = GetTable<T>().Where(predicate);
            return sql.ToListAsync();
        }

        public Task<List<T>> SelectAllAsync<T>()
            where T : class
        {
            return GetTable<T>().ToListAsync<T>();
        }

        public Task<bool> ExistsAsync<T>()
            where T : class
        {
            return GetTable<T>().AnyAsync();
        }

        public Task<bool> ExistsAsync<T>(Expression<Func<T, bool>> predicate)
            where T : class
        {
            return GetTable<T>().AnyAsync(predicate);
        }

        public Task<long> CountAsync<T>(Expression<Func<T, bool>> predicate)
            where T : class
        {
            return GetTable<T>().LongCountAsync(predicate);
        }

        public Task<long> CountAsync<T>()
            where T : class
        {
            return GetTable<T>().LongCountAsync<T>();
        }

        public async Task<DbReturnListModel<T>> SelectByPageWithTotalRowsAsync<T>(int pageNumber, int pageSize, Expression<Func<T, bool>> predicate = null)
            where T : class
        {
            if (pageSize == 0)
                pageSize = 100;
            if (pageNumber <= 0)
                pageNumber = 1;

            var querable = GetTable<T>().Skip(pageSize * (pageNumber - 1)).Take(pageSize);

            long totalItems;
            if (predicate != null)
            {
                querable = querable.Where(predicate);
                totalItems = await GetTable<T>().CountAsync(predicate);
            }
            else
            {
                totalItems = await GetTable<T>().CountAsync();
            }

            return new DbReturnListModel<T>(await querable.ToListAsync(), totalItems);
        }

        public Task<List<T>> SelectByPageAsync<T>(int pageNumber, int pageSize, Expression<Func<T, bool>> predicate = null)
            where T : class
        {
            if (pageSize == 0)
                pageSize = 100;

            if (pageNumber <= 0)
                pageNumber = 1;

            var querable = GetTable<T>().Skip(pageSize * (pageNumber - 1)).Take(pageSize);

            if (predicate != null)
            {
                querable = querable.Where(predicate);
            }

            return querable.ToListAsync();
        }

        public async Task<List<object>> InsertAllAsync<T>(List<T> list)
            where T : class
        {
            var lst = new List<object>();
            foreach (var item in list)
            {
                lst.Add(await InsertAsync(item));
            }

            return lst;
        }

        public async Task<List<TKey>> InsertAllAsync<T, TKey>(List<T> list)
            where T : class
            where TKey : struct
        {
            var lst = new List<TKey>();
            foreach (var itemKey in await InsertAsync<T>(list))
            {
                lst.Add((TKey)itemKey);
            }

            return lst;
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            log.Debug($"Destroying Instance of {this.GetType()} with HashCode - {this.GetHashCode()}");

            if (disposing)
            {
                if (commonTransaction != null)
                {
                    commonTransaction.Dispose();
                    commonTransaction = null;
                }

                if ((connection != null) && (connection.Connection?.State == ConnectionState.Open))
                    connection.Close();

                connection?.Dispose();
            }

            disposed = true;
        }

        private void ToggleLogging()
        {
#pragma warning disable S2696 // Instance members should not write to "static" fields
            LinqToDB.Common.Configuration.AvoidSpecificDataProviderAPI = true;
#pragma warning restore S2696 // Instance members should not write to "static" fields

            if (logConfig.LogSql)
            {
                DataConnection.TurnTraceSwitchOn(System.Diagnostics.TraceLevel.Verbose);
                DataConnection.OnTrace = info =>
                {
                    if (info.TraceInfoStep != TraceInfoStep.AfterExecute)
                        return;

                    var profiledDbCommand = info.Command;

                    var result = string.Empty;
                    result = info.RecordsAffected.ToString();

                    var ptxt = new StringBuilder();
                    foreach (DbParameter param in profiledDbCommand.Parameters)
                    {
                        ptxt.AppendFormat("{2} {0} = {1} ", param.ParameterName, param.Value, ptxt.Length > 0 ? "," : string.Empty);
                    }

                    var parameterString = ptxt.ToString();

                    if (info.Exception == null)
                        log.Sql((profiledDbCommand.CommandType == CommandType.StoredProcedure ? "SP - " : string.Empty) + profiledDbCommand.CommandText + (parameterString.Length > 0 ? ("--" + parameterString) : string.Empty) + " -- hashcode = " + this.GetHashCode(), result, info.ExecutionTime ?? new TimeSpan(0));
                    else
                        log.SqlError(info.Exception, (profiledDbCommand.CommandType == CommandType.StoredProcedure ? "SP - " : string.Empty) + profiledDbCommand.CommandText + (parameterString.Length > 0 ? ("--" + parameterString) : string.Empty) + " -- hashcode = " + this.GetHashCode());
                };
            }
            else
            {
                DataConnection.TurnTraceSwitchOn(System.Diagnostics.TraceLevel.Off);
            }
        }

        private void EnsureOpenConnection()
        {
            if ((connection == null) || ((connection.Connection == null) || (connection.Connection.State == ConnectionState.Closed || connection.Connection.State == ConnectionState.Broken)))
            {
                connection?.Dispose();
                connection = new DataConnection(dbInfo.GetDBProvider(), ConnectionString)
                {
                    CommandTimeout = dbInfo.GetDbSettings().DatabaseCommandTimeout
                };
            }
        }
    }
}
