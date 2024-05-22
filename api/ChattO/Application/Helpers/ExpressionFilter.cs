using System.Linq.Expressions;

namespace Application.Helpers;

public static class ExpressionFilter<TResult, TSource> 
    where TResult : class
    where TSource : class
{
    private class Filter
    {
        public string ColumnName { get; set; }
        public string Value { get; set; }
    }
    public static Expression<Func<TResult, bool>> GetFilter(TSource sourсe)
    {
        Expression<Func<TResult, bool>> filters = null;

        try
        {
            var parameter = Expression.Parameter(typeof(TResult), nameof(TResult));
            var columnFilters = GetFilters(sourсe);

            Expression filterExpression = null;

            foreach(var columnFilter in columnFilters)
            {
                var property = Expression.Property(parameter, columnFilter.ColumnName);
                var comparison = GetComparisonExpression(property, columnFilter);

                filterExpression = filterExpression == null ? comparison : Expression.And(filterExpression, comparison);
            }

            filters = Expression.Lambda<Func<TResult, bool>>(filterExpression, parameter);
        }
        catch
        {
            filters = null;
        }

        return filters;
    }

    private static List<Filter> GetFilters(TSource request)
    {
        var properties = typeof(TSource)
            .GetProperties()
            .Where(p => p.IsDefined(typeof(FilterAttribute), false));

        var filters = properties.Aggregate(new List<Filter>(), (list, property) =>
        {
            var value = property.GetValue(request);
            if (value != null)
            {
                list.Add(new Filter()
                {
                    ColumnName = property.Name,
                    Value = value.ToString(),
                });
            }
                                
            return list;
        });

        return filters;
    }

    private static Expression GetComparisonExpression(MemberExpression property, Filter columnFilter)
    {
        Expression comparison;

        if (property.Type == typeof(string))
        {
            var dbValueToUpper = Expression.Call(property, "ToUpper", Type.EmptyTypes);
            var queryValueToUpper = Expression.Constant(columnFilter.Value.ToUpper());
            comparison = Expression.Call(dbValueToUpper, "Contains", Type.EmptyTypes, queryValueToUpper);
        }
        else if (property.Type == typeof(DateTime))
        {
            var date = DateTime.Parse(columnFilter.Value);
            comparison = Expression.Equal(property, Expression.Constant(date));
        }
        else if (property.Type == typeof(Guid))
        {
            var guid = Guid.Parse(columnFilter.Value);
            comparison = Expression.Equal(property, Expression.Constant(guid));
        }
        else if (property.Type == typeof(double))
        {
            comparison = Expression.Equal(property, Expression.Constant(Convert.ToDouble(columnFilter.Value)));
        }
        else if (property.Type.IsEnum)
        {
            comparison = Expression.Equal(property, Expression.Constant(Enum.Parse(property.Type, columnFilter.Value)));
        }
        else
        {
            comparison = Expression.Equal(property, Expression.Constant(Convert.ToInt32(columnFilter.Value)));
        }

        return comparison;
    }
}
