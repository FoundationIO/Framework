/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Framework.Infrastructure.Utils
{
    public static class LinqUtils
    {
        public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> obj, int pageNumber, int pageSize)
        {
            if (pageSize == 0)
            {
                pageSize = 100;
            }

            if (pageNumber == 0)
            {
                return obj.Take(pageSize);
            }

            pageNumber -= 1; //Skip records of previous pages only
            return obj.Skip(pageSize * pageNumber).Take(pageSize);
        }

        public static void ForEachWithIndex<T>(this IEnumerable<T> enumerable, Action<T, int> handler)
        {
            if (enumerable == null)
            {
                return;
            }

            var idx = 0;
            foreach (var item in enumerable)
            {
                handler(item, idx++);
            }
        }

        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            if (enumerable == null)
            {
                return;
            }

            foreach (var item in enumerable)
            {
                action(item);
            }
        }

        public static Expression<Func<T, bool>> Or<T>(
            this Expression<Func<T, bool>> expr1,
            Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>(Expression.OrElse(expr1.Body, invokedExpr), expr1.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(
            this Expression<Func<T, bool>> expr1,
            Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(expr1.Body, invokedExpr), expr1.Parameters);
        }

        public static Expression<Func<T, bool>> True<T>()
        {
            return f => true;
        }

        public static Expression<Func<T, bool>> False<T>()
        {
            return f => false;
        }

        public static Expression<Func<T, TType>> FuncToExpression<T, TType>(Func<T, TType> f)
        {
            return x => f(x);
        }

        public static Expression<Func<T, bool>> AddFilterToStringProperty<T>(Expression<Func<T, string>> expression, string filter, string fiterFunctionName)
        {
            var notNullExpresion = Expression.NotEqual(expression.Body, Expression.Constant(null));

            var methodExpresion = Expression.Call(
                    expression.Body,
                    fiterFunctionName,
                    null,
                    Expression.Constant(filter));

            var filterExpresion = Expression.AndAlso(notNullExpresion, methodExpresion);
            return Expression.Lambda<Func<T, bool>>(filterExpresion, expression.Parameters);
        }

        public static IQueryable<T> ApplySorting<T>(this IQueryable<T> expression, string column, bool isAsc)
        {
            var param = Expression.Parameter(typeof(T), typeof(T).Name);
            Expression<Func<T, object>> orderExpression;
            try
            {
                orderExpression = Expression.Lambda<Func<T, object>>(Expression.Property(param, column), param);
            }
            catch
            {
                return expression;
            }

            if (orderExpression == null)
                return expression;

            return isAsc ? expression.OrderBy(orderExpression) : expression.OrderByDescending(orderExpression);
        }

        public static IOrderedQueryable<TSource> ApplySortingOrderBy<TSource>(this IQueryable<TSource> query, string propertyName,bool isAsc)
        {
            try
            {
                var entityType = typeof(TSource);
                var propertyInfo = entityType.GetProperty(propertyName);

                ParameterExpression parameterExpression = Expression.Parameter(entityType, "x");
                MemberExpression property = Expression.Property(parameterExpression, propertyName);

                var selector = Expression.Lambda(property, new ParameterExpression[] { parameterExpression });
                var enumarableType = typeof(System.Linq.Queryable);
                var method = GetMethodInfo(isAsc, enumarableType).Single();

                MethodInfo genericMethod = method
                     .MakeGenericMethod(entityType, propertyInfo.PropertyType);

                var newQuery = (IOrderedQueryable<TSource>)genericMethod
                     .Invoke(genericMethod, new object[] { query, selector });

                return newQuery;
            }
            catch
            {
                return (IOrderedQueryable<TSource>)query;
            }
        }

        private static IEnumerable<MethodInfo> GetMethodInfo(bool isAsc, Type enumarableType)
        {
            return enumarableType.GetMethods()
                             .Where(m => m.Name == (isAsc ? "OrderBy" : "OrderByDescending") && m.IsGenericMethodDefinition)
                             .Where(m =>
                             {
                                 var parameters = m.GetParameters().ToList();
                                 return parameters.Count == 2;
                             });
        }
    }
}