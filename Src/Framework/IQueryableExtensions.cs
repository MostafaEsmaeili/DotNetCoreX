using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Framework.Entity;

namespace Framework
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> SetOrderByExpression<T>(this IQueryable<T> builder, IEnumerable<IPageSortRequest> request) where T : class, new()
        {
            var pageSortRequests = request.ToList();
            if (!pageSortRequests.Any())
            {
                var ex = "Id".CreatePropertyAccessor<T, object>();
                var orderedQueryable = builder.OrderBy(ex);
                return orderedQueryable;
            }
            else
            {
                foreach (var sort in pageSortRequests)
                {
                    if(string.IsNullOrEmpty(sort.Field))
                    {
                        sort.Field = "Id";
                    }
                    if (sort.Direction.ToLower() =="asc")
                    {
                        var ex = sort.Field.CreatePropertyAccessor<T, object>();
                        return builder.OrderBy(ex);
                    }
                    else
                    {
                        var ex = sort.Field.CreatePropertyAccessor<T, object>();
                        return builder.OrderByDescending(ex);
                    }

                }
                return builder;
            }
        }

        public static Expression<Func<TIn, TOut>> CreatePropertyAccessor<TIn, TOut> (this string propertyName) where  TOut: class
        {
            var param = Expression.Parameter(typeof(TIn));
            var body = Expression.PropertyOrField(param, propertyName);
            var convertToObject = Expression.Convert(body, typeof(object));

            return Expression.Lambda<Func<TIn, TOut>>(convertToObject, param);
        }
    }
}