﻿using System.Linq.Expressions;

namespace Application.Helpers;

public class SortingBuilder<T> where T : class
{
    public static Result<Expression<Func<IQueryable<T>, IOrderedQueryable<T>>>> GetSortBy(string propertyName, bool isDescending)
    {
        try
        {
            var queryableParameter = Expression.Parameter(typeof(IQueryable<T>), "query");
            var entityParameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(entityParameter, propertyName);
            var lambda = Expression.Lambda(property, entityParameter);
            var methodName = isDescending ? "OrderByDescending" : "OrderBy";

            var method = typeof(Queryable).GetMethods()
                .First(m => m.Name == methodName
                            && m.GetParameters().Length == 2);

            var genericMethod = method.MakeGenericMethod(typeof(T), property.Type);

            var methodCallExpression = Expression.Call(
                genericMethod,
                queryableParameter,
                lambda
            );

            return Result.Success(Expression.Lambda<Func<IQueryable<T>, IOrderedQueryable<T>>>(methodCallExpression, queryableParameter));
        }
        catch
        {
            return Result.Failure<Expression<Func<IQueryable<T>, IOrderedQueryable<T>>>>($"Invalid sorting property name: {propertyName}");
        }
    }
}
