/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Framework.Infrastructure.Constants;
using Framework.Infrastructure.Interfaces.DbAccess;
using Framework.Infrastructure.Models.Result;
using Framework.Infrastructure.Models.Search;
using Framework.Infrastructure.Utils;
using LinqToDB;

namespace Framework.Data.Utils
{
    public static class RepositoryUtils
    {
        public static void UpdateAuditInfo(this IAuditableModel model, string createdBy, string modifiedBy)
        {
            model.CreatedBy = createdBy;
            model.CreatedDate = DateTime.UtcNow;
            model.ModifiedBy = modifiedBy;
            model.ModifiedDate = DateTime.UtcNow;
        }

        public static void UpdateModifiedAuditInfo(this IAuditableModel model, string modifiedBy)
        {
            model.ModifiedBy = modifiedBy;
            model.ModifiedDate = DateTime.UtcNow;
        }

        public static async Task<DbReturnListModel<T>> ReturnListModelResultAsync<T>(this IQueryable<T> sql, IBaseSearchCriteria baseSearchCriteria)
        {
            return new DbReturnListModel<T>(await sql.AsQueryable().ApplyPaging(baseSearchCriteria.Page, baseSearchCriteria.PageSize).ToListAsync(), await sql.AsQueryable().LongCountAsync());
        }

        public static async Task<DbReturnListModel<T>> ReturnListModelResultAsync<T>(this IQueryable<T> sql)
        {
            var data = await sql.AsQueryable().ToListAsync();
            return new DbReturnListModel<T>(data, data.LongCount());
        }

        public static DbReturnListModel<T> ReturnListModelResult<T>(this IQueryable<T> sql, IBaseSearchCriteria baseSearchCriteria)
        {
            baseSearchCriteria.SortBy
            return new DbReturnListModel<T>(sql.AsQueryable().ApplyPaging(baseSearchCriteria.Page, baseSearchCriteria.PageSize).ToList(), sql.AsQueryable().LongCount());
        }

        public static DbReturnListModel<T> ReturnListModelResult<T>(this IQueryable<T> sql)
        {
            var data = sql.AsQueryable().ToList();
            return new DbReturnListModel<T>(data, data.LongCount());
        }

        public static IQueryable<T> ApplyFilterOperators<T>(this IQueryable<T> qry, long filterOperator, Func<T, string> keySelector, string keyValue)
        {
            qry = ApplyFilterOperators<T, string>(qry, filterOperator, keySelector, keyValue);

            var keySelectorExp = LinqUtils.FuncToExpression(keySelector);
            Expression<Func<T, bool>> exp;
            if (filterOperator == FilterOperatorContants.StartsWith || filterOperator == FilterOperatorContants.NotStartsWith)
            {
                exp = LinqUtils.AddFilterToStringProperty(keySelectorExp, keyValue, "StartsWith");
                if (filterOperator == FilterOperatorContants.NotStartsWith)
                    exp = Expression.Lambda<Func<T, bool>>(Expression.Not(exp), exp.Parameters);
            }
            else if (filterOperator == FilterOperatorContants.EndsWith || filterOperator == FilterOperatorContants.NotEndsWith)
            {
                exp = LinqUtils.AddFilterToStringProperty(keySelectorExp, keyValue, "EndsWith");
                if (filterOperator == FilterOperatorContants.NotEndsWith)
                    exp = Expression.Lambda<Func<T, bool>>(Expression.Not(exp), exp.Parameters);
            }
            else if (filterOperator == FilterOperatorContants.Contains || filterOperator == FilterOperatorContants.NotContains)
            {
                exp = LinqUtils.AddFilterToStringProperty(keySelectorExp, keyValue, "Contains");
                if (filterOperator == FilterOperatorContants.NotEndsWith)
                    exp = Expression.Lambda<Func<T, bool>>(Expression.Not(exp), exp.Parameters);
            }
            else
            {
                return qry;
            }

            qry = qry.Where(exp);
            return qry;
        }

        public static IQueryable<T> ApplyFilterOperators<T,TKeyType>(this IQueryable<T> qry, long filterOperator, Func<T, TKeyType> keySelector, TKeyType keyValue)
        {
            var argument = Expression.Parameter(typeof(TKeyType));
            var keySelectorExp = LinqUtils.FuncToExpression(keySelector);
            BinaryExpression exp;

            if (filterOperator == FilterOperatorContants.EqualTo)
            {
                exp = Expression.Equal(keySelectorExp, Expression.Constant(keyValue));
            }
            else if (filterOperator == FilterOperatorContants.NotEqualTo)
            {
                exp = Expression.NotEqual(keySelectorExp, Expression.Constant(keyValue));
            }
            else if (filterOperator == FilterOperatorContants.GreaterThan)
            {
                exp = Expression.GreaterThan(keySelectorExp, Expression.Constant(keyValue));
            }
            else if (filterOperator == FilterOperatorContants.GreaterThanOrEqualTo)
            {
                exp = Expression.GreaterThanOrEqual(keySelectorExp, Expression.Constant(keyValue));
            }
            else if (filterOperator == FilterOperatorContants.LessThan)
            {
                exp = Expression.LessThan(keySelectorExp, Expression.Constant(keyValue));
            }
            else if (filterOperator == FilterOperatorContants.LessThanAndEqualTo)
            {
                exp = Expression.LessThanOrEqual(keySelectorExp, Expression.Constant(keyValue));
            }
            else
            {
                return qry;
            }

            qry = qry.Where(Expression.Lambda<Func<T, bool>>(exp, new[] { argument }));

            return qry;
        }
    }
}
