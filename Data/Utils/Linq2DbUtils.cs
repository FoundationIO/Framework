/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Framework.Infrastructure.Interfaces.DbAccess;
using LinqToDB.Linq;

namespace LinqToDB
{
    public static class Linq2DbUtils
    {
        public static int UpdateWithAudit<T>(this IDataContext dataContext, T obj, string modifiedBy, string tableName = null, string databaseName = null, string schemaName = null)
            where T : IAuditableModel
        {
            obj.ModifiedBy = modifiedBy;
            obj.ModifiedDate = DateTime.UtcNow;
            return dataContext.Update<T>(obj, tableName, databaseName, schemaName);
        }

        public static int UpdateWithAudit<T>(this IQueryable<T> source, Expression<Func<T, T>> setter, string modifiedBy)
            where T : IAuditableModel
        {
            source.Set(x => x.ModifiedDate , DateTime.UtcNow);
            source.Set(x => x.ModifiedBy, modifiedBy);
            return source.Update<T>(setter);
        }

        public static int UpdateWithAudit<TSource, TTarget>(this IQueryable<TSource> source, ITable<TTarget> target, Expression<Func<TSource, TTarget>> setter, string modifiedBy)
            where TSource : IAuditableModel
        {
            source.Set(x => x.ModifiedDate, DateTime.UtcNow);
            source.Set(x => x.ModifiedBy, modifiedBy);
            return source.Update(target, setter);
        }

        public static int UpdateWithAudit<TSource, TTarget>(this IQueryable<TSource> source, Expression<Func<TSource, TTarget>> target, Expression<Func<TSource, TTarget>> setter, string modifiedBy)
            where TSource : IAuditableModel
        {
            source.Set(x => x.ModifiedDate, DateTime.UtcNow);
            source.Set(x => x.ModifiedBy, modifiedBy);

            return source.Update(target, setter);
        }

        public static int UpdateWithAudit<T>(this IQueryable<T> source, Expression<Func<T, bool>> predicate, Expression<Func<T, T>> setter, string modifiedBy)
            where T : IAuditableModel
        {
            source.Set(x => x.ModifiedDate, DateTime.UtcNow);
            source.Set(x => x.ModifiedBy, modifiedBy);

            return source.Update<T>(predicate, setter);
        }

        public static int UpdateWithAudit<T>(this IUpdatable<T> source, string modifiedBy)
            where T : IAuditableModel
        {
            source.Set(x => x.ModifiedDate, DateTime.UtcNow);
            source.Set(x => x.ModifiedBy, modifiedBy);

            return source.Update<T>();
        }

        public static Task<int> UpdateWithAuditAsync<T>(this IDataContext dataContext, T obj, string modifiedBy, string tableName = null, string databaseName = null, string schemaName = null, CancellationToken token = default)
            where T : IAuditableModel
        {
            obj.ModifiedBy = modifiedBy;
            obj.ModifiedDate = DateTime.UtcNow;
            return dataContext.UpdateAsync<T>(obj, tableName, databaseName, schemaName, token);
        }

        public static Task<int> UpdateWithAuditAsync<TSource, TTarget>(this IQueryable<TSource> source, Expression<Func<TSource, TTarget>> target, Expression<Func<TSource, TTarget>> setter, string modifiedBy , CancellationToken token = default)
            where TSource : IAuditableModel
        {
            source.Set(x => x.ModifiedDate, DateTime.UtcNow);
            source.Set(x => x.ModifiedBy, modifiedBy);

            return source.UpdateAsync(target, setter, token);
        }

        public static Task<int> UpdateWithAuditAsync<TSource, TTarget>(this IQueryable<TSource> source, ITable<TTarget> target, Expression<Func<TSource, TTarget>> setter, string modifiedBy , CancellationToken token = default)
            where TSource : IAuditableModel
        {
            source.Set(x => x.ModifiedDate, DateTime.UtcNow);
            source.Set(x => x.ModifiedBy, modifiedBy);

            return source.UpdateAsync(target, setter, token);
        }

        public static Task<int> UpdateWithAuditAsync<T>(this IQueryable<T> source, Expression<Func<T, bool>> predicate, Expression<Func<T, T>> setter, string modifiedBy, CancellationToken token = default)
            where T : IAuditableModel
        {
            source.Set(x => x.ModifiedDate, DateTime.UtcNow);
            source.Set(x => x.ModifiedBy, modifiedBy);

            return source.UpdateAsync<T>(predicate, setter, token);
        }

        public static Task<int> UpdateWithAuditAsync<T>(this IQueryable<T> source, Expression<Func<T, T>> setter, string modifiedBy, CancellationToken token = default)
            where T : IAuditableModel
        {
            source.Set(x => x.ModifiedDate, DateTime.UtcNow);
            source.Set(x => x.ModifiedBy, modifiedBy);

            return source.UpdateAsync<T>(setter, token);
        }

        public static Task<int> UpdateWithAuditAsync<T>(this IUpdatable<T> source, string modifiedBy, CancellationToken token = default)
            where T : IAuditableModel
        {
            source.Set(x => x.ModifiedDate, DateTime.UtcNow);
            source.Set(x => x.ModifiedBy, modifiedBy);

            return source.UpdateAsync<T>(token);
        }

        public static int InsertWithAudit<T>(this IDataContext dataContext, T obj, string createdBy, string modifiedBy, string tableName = null, string databaseName = null, string schemaName = null)
            where T : IAuditableModel
        {
            obj.CreatedBy = createdBy;
            obj.CreatedDate = DateTime.UtcNow;
            obj.ModifiedBy = modifiedBy;
            obj.ModifiedDate = DateTime.UtcNow;
            return dataContext.Insert<T>(obj, tableName, databaseName, schemaName);
        }

        public static Task<int> InsertWithAuditAsync<T>(this IDataContext dataContext, T obj, string createdBy, string modifiedBy, string tableName = null, string databaseName = null, string schemaName = null, CancellationToken token = default)
            where T : IAuditableModel
        {
            obj.CreatedBy = createdBy;
            obj.CreatedDate = DateTime.UtcNow;
            obj.ModifiedBy = modifiedBy;
            obj.ModifiedDate = DateTime.UtcNow;
            return dataContext.InsertAsync<T>(obj, tableName , databaseName , schemaName, token);
        }

        public static int InsertOrReplaceWithAudit<T>(this IDataContext dataContext, T obj, string createdBy, string modifiedBy, string tableName = null, string databaseName = null, string schemaName = null)
            where T : IAuditableModel
        {
            obj.CreatedBy = createdBy;
            obj.CreatedDate = DateTime.UtcNow;
            obj.ModifiedBy = modifiedBy;
            obj.ModifiedDate = DateTime.UtcNow;
            return dataContext.InsertOrReplace<T>(obj, tableName, databaseName, schemaName);
        }

        public static Task<int> InsertOrReplaceWithAuditAsync<T>(this IDataContext dataContext, T obj, string createdBy, string modifiedBy, string tableName = null, string databaseName = null, string schemaName = null, CancellationToken token = default)
            where T : IAuditableModel
        {
            obj.CreatedBy = createdBy;
            obj.CreatedDate = DateTime.UtcNow;
            obj.ModifiedBy = modifiedBy;
            obj.ModifiedDate = DateTime.UtcNow;
            return dataContext.InsertOrReplaceAsync<T>(obj, tableName, databaseName, schemaName, token);
        }

        public static decimal InsertWithDecimalIdentityWithAudit<T>(this IDataContext dataContext, T obj, string createdBy, string modifiedBy, string tableName = null, string databaseName = null, string schemaName = null)
            where T : IAuditableModel
        {
            obj.CreatedBy = createdBy;
            obj.CreatedDate = DateTime.UtcNow;
            obj.ModifiedBy = modifiedBy;
            obj.ModifiedDate = DateTime.UtcNow;
            return dataContext.InsertWithDecimalIdentity<T>(obj, tableName, databaseName, schemaName);
        }

        public static Task<decimal> InsertWithDecimalIdentityWithAuditAsync<T>(this IDataContext dataContext, T obj, string createdBy, string modifiedBy, string tableName = null, string databaseName = null, string schemaName = null, CancellationToken token = default)
            where T : IAuditableModel
        {
            obj.CreatedBy = createdBy;
            obj.CreatedDate = DateTime.UtcNow;
            obj.ModifiedBy = modifiedBy;
            obj.ModifiedDate = DateTime.UtcNow;
            return dataContext.InsertWithDecimalIdentityAsync<T>(obj,tableName, databaseName, schemaName, token);
        }

        public static object InsertWithIdentityWithAudit<T>(this IDataContext dataContext, T obj, string createdBy, string modifiedBy, string tableName = null, string databaseName = null, string schemaName = null)
            where T : IAuditableModel
        {
            obj.CreatedBy = createdBy;
            obj.CreatedDate = DateTime.UtcNow;
            obj.ModifiedBy = modifiedBy;
            obj.ModifiedDate = DateTime.UtcNow;
            return dataContext.InsertWithIdentity<T>(obj, tableName, databaseName, schemaName);
        }

        public static Task<object> InsertWithIdentityWithAuditAsync<T>(this IDataContext dataContext, T obj, string createdBy, string modifiedBy, string tableName = null, string databaseName = null, string schemaName = null, CancellationToken token = default)
            where T : IAuditableModel
        {
            obj.CreatedBy = createdBy;
            obj.CreatedDate = DateTime.UtcNow;
            obj.ModifiedBy = modifiedBy;
            obj.ModifiedDate = DateTime.UtcNow;
            return dataContext.InsertWithIdentityAsync<T>(obj, tableName, databaseName, schemaName, token);
        }

        public static int InsertWithInt32IdentityWithAudit<T>(this IDataContext dataContext, T obj, string createdBy, string modifiedBy, string tableName = null, string databaseName = null, string schemaName = null)
            where T : IAuditableModel
        {
            obj.CreatedBy = createdBy;
            obj.CreatedDate = DateTime.UtcNow;
            obj.ModifiedBy = modifiedBy;
            obj.ModifiedDate = DateTime.UtcNow;
            return dataContext.InsertWithInt32Identity<T>(obj, tableName, databaseName, schemaName);
        }

        public static Task<int> InsertWithInt32IdentityWithAuditAsync<T>(this IDataContext dataContext, T obj, string createdBy, string modifiedBy, string tableName = null, string databaseName = null, string schemaName = null, CancellationToken token = default)
            where T : IAuditableModel
        {
            obj.CreatedBy = createdBy;
            obj.CreatedDate = DateTime.UtcNow;
            obj.ModifiedBy = modifiedBy;
            obj.ModifiedDate = DateTime.UtcNow;
            return dataContext.InsertWithInt32IdentityAsync<T>(obj, tableName,databaseName, schemaName, token);
        }

        public static long InsertWithInt64IdentityWithAudit<T>(this IDataContext dataContext, T obj, string createdBy, string modifiedBy, string tableName = null, string databaseName = null, string schemaName = null)
            where T : IAuditableModel
        {
            obj.CreatedBy = createdBy;
            obj.CreatedDate = DateTime.UtcNow;
            obj.ModifiedBy = modifiedBy;
            obj.ModifiedDate = DateTime.UtcNow;
            return dataContext.InsertWithInt64Identity<T>(obj, tableName, databaseName, schemaName);
        }

        public static Task<long> InsertWithInt64IdentityWithAuditAsync<T>(this IDataContext dataContext, T obj, string createdBy, string modifiedBy, string tableName = null, string databaseName = null, string schemaName = null, CancellationToken token = default)
            where T : IAuditableModel
        {
            obj.CreatedBy = createdBy;
            obj.CreatedDate = DateTime.UtcNow;
            obj.ModifiedBy = modifiedBy;
            obj.ModifiedDate = DateTime.UtcNow;
            return dataContext.InsertWithInt64IdentityAsync<T>(obj, tableName, databaseName,schemaName, token);
        }
    }
}
