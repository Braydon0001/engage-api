using System.Reflection;

namespace Engage.Application.Pagination;

public static class PaginationUtils
{
    ///////////////////////////////
    /// Filtering New (AgGrid)
    ///////////////////////////////

    /// See https://blog.jeremylikness.com/blog/dynamically-build-linq-expressions/

    public static Expression<Func<TEntity, bool>> CreateFilter<TEntity>(KeyValuePair<string, FilterModel> filter, string propertyName)
    {
        return filter.Value.FilterType switch
        {
            FilterType.BOOLEAN => BoolFilter<TEntity>(filter, propertyName),
            FilterType.DATE => DateFilter<TEntity>(filter, propertyName),
            // TODO NumerFilter
            FilterType.NUMBER => IntFilter<TEntity>(filter, propertyName),
            FilterType.TEXT => TextFilter<TEntity>(filter, propertyName),
            // TODO SetFilter 
            FilterType.SET => IntContainsFilter<TEntity>(filter, propertyName),
            _ => throw new PaginationException("Unknown Filter Type"),
        };
    }

    public static Expression<Func<TEntity, bool>> BoolFilter<TEntity>(KeyValuePair<string, FilterModel> filter, string propertyName)
    {
        var parameter = Expression.Parameter(typeof(TEntity));
        var property = GetProperty(parameter, propertyName);
        var value = Expression.Constant(bool.Parse(filter.Value.Filter));
        var equal = Expression.Equal(property, value);

        return (Expression<Func<TEntity, bool>>)Expression.Lambda(equal, parameter);
    }

    public static Expression<Func<TEntity, bool>> DateFilter<TEntity>(KeyValuePair<string, FilterModel> filter, string propertyName)
    {
        var operation = filter.Value.Type.ToUpper();
        var parameter = Expression.Parameter(typeof(TEntity));
        var property = GetProperty(parameter, propertyName + ".Date");
        var value = filter.Value.DateFrom.Value.Date;

        BinaryExpression binary;
        if (operation == DateOperator.IN_RANGE)
        {
            var toValue = filter.Value.DateTo.Value.Date;
            if (toValue < value)
            {
                (value, toValue) = (toValue, value);
            }
            var from = Expression.Constant(value);
            var to = Expression.Constant(toValue);
            var left = Expression.GreaterThanOrEqual(property, from);
            var right = Expression.LessThanOrEqual(property, to);
            binary = Expression.AndAlso(left, right);
        }
        else
        {
            var val = Expression.Constant(value);
            binary = operation switch
            {
                DateOperator.EQUALS => Expression.Equal(property, val),
                DateOperator.NOT_EQUAL => Expression.NotEqual(property, val),
                DateOperator.LESS_THAN => Expression.LessThan(property, val),
                DateOperator.GREATER_THAN => Expression.GreaterThan(property, val),
                _ => throw new PaginationException("Unknown DateOperator")

            };
        }
        return (Expression<Func<TEntity, bool>>)Expression.Lambda(binary, parameter);
    }

    // NumberFilters 
    public static Expression<Func<TEntity, bool>> IntFilter<TEntity>(KeyValuePair<string, FilterModel> filter, string propertyName)
    {
        var operation = filter.Value.Type.ToUpper();
        var parameter = Expression.Parameter(typeof(TEntity));
        var property = GetProperty(parameter, propertyName);
        //TODO Get typeof property 
        //property.GetType()        
        var value = int.Parse(filter.Value.Filter);

        BinaryExpression binary;
        if (operation == NumberOperator.IN_RANGE)
        {
            //TODO Generic Method
            //     Switch Case
            var toValue = int.Parse(filter.Value.FilterTo);
            if (toValue < value)
            {
                (value, toValue) = (toValue, value);
            }
            var from = Expression.Constant(value);
            var to = Expression.Constant(toValue);
            var left = Expression.GreaterThanOrEqual(property, from);
            var right = Expression.LessThanOrEqual(property, to);
            binary = Expression.AndAlso(left, right);
        }
        else
        {
            var val = Expression.Constant(value);
            binary = operation switch
            {
                NumberOperator.EQUALS => Expression.Equal(property, val),
                NumberOperator.NOT_EQUAL => Expression.NotEqual(property, val),
                NumberOperator.LESS_THAN => Expression.LessThan(property, val),
                NumberOperator.LESS_THAN_OR_EQUAL => Expression.LessThanOrEqual(property, val),
                NumberOperator.GREATER_THAN => Expression.GreaterThan(property, val),
                NumberOperator.GREATER_THAN_OR_EQUAL => Expression.GreaterThanOrEqual(property, val),
                _ => throw new PaginationException("Unknown NumberOperator"),
            };
        }
        return (Expression<Func<TEntity, bool>>)Expression.Lambda(binary, parameter);
    }
    public static Expression<Func<TEntity, bool>> DecimalFilter<TEntity>(KeyValuePair<string, FilterModel> filter, string propertyName)
    {
        var operation = filter.Value.Type.ToUpper();
        var parameter = Expression.Parameter(typeof(TEntity));
        var property = GetProperty(parameter, propertyName);
        var value = decimal.Parse(filter.Value.Filter);

        BinaryExpression binary;
        if (operation == NumberOperator.IN_RANGE)
        {
            var toValue = decimal.Parse(filter.Value.FilterTo);
            if (toValue < value)
            {
                (value, toValue) = (toValue, value);
            }
            var from = Expression.Constant(value);
            var to = Expression.Constant(toValue);
            var left = Expression.GreaterThanOrEqual(property, from);
            var right = Expression.LessThanOrEqual(property, to);
            binary = Expression.AndAlso(left, right);
        }
        else
        {
            var val = Expression.Constant(value);
            binary = operation switch
            {
                NumberOperator.EQUALS => Expression.Equal(property, val),
                NumberOperator.NOT_EQUAL => Expression.NotEqual(property, val),
                NumberOperator.LESS_THAN => Expression.LessThan(property, val),
                NumberOperator.LESS_THAN_OR_EQUAL => Expression.LessThanOrEqual(property, val),
                NumberOperator.GREATER_THAN => Expression.GreaterThan(property, val),
                NumberOperator.GREATER_THAN_OR_EQUAL => Expression.GreaterThanOrEqual(property, val),
                _ => throw new PaginationException("Unknown NumberOperator"),
            };
        }
        return (Expression<Func<TEntity, bool>>)Expression.Lambda(binary, parameter);
    }

    public static Expression<Func<TEntity, bool>> IntContainsFilter<TEntity>(KeyValuePair<string, FilterModel> filter, string propertyName)
    {
        var parameter = Expression.Parameter(typeof(TEntity));
        var property = GetProperty(parameter, propertyName);
        var value = Expression.Constant(filter.Value.Values);

        var contains = EnumerableContains().MakeGenericMethod(typeof(int));

        var methodCall = Expression.Call(contains, value, property);

        return (Expression<Func<TEntity, bool>>)Expression.Lambda(methodCall, parameter);
    }

    public static Expression<Func<TEntity, bool>> TextFilter<TEntity>(KeyValuePair<string, FilterModel> filter, string propertyName)
    {
        var operation = filter.Value.Type.ToUpper();
        var parameter = Expression.Parameter(typeof(TEntity));
        var property = GetProperty(parameter, propertyName);
        var value = Expression.Constant(filter.Value.Filter);

        var lambda = operation switch
        {
            TextOperator.EQUALS => Expression.Lambda(Expression.Equal(property, value), parameter),
            TextOperator.NOT_EQUAL => Expression.Lambda(Expression.NotEqual(property, value), parameter),
            TextOperator.CONTAINS => Expression.Lambda(Expression.Call(property, StringContains(), value), parameter),
            TextOperator.NOT_CONTAINS => Expression.Lambda(Expression.Not(Expression.Call(property, StringContains(), value)), parameter),
            TextOperator.STARTS_WITH => Expression.Lambda(Expression.Call(property, StringStartsWith(), value), parameter),
            TextOperator.ENDS_WITH => Expression.Lambda(Expression.Call(property, StringEndsWith(), value), parameter),
            _ => throw new PaginationException("Unknown TextOperator")
        };

        return (Expression<Func<TEntity, bool>>)lambda;
    }

    private static MemberExpression GetProperty(ParameterExpression parameter, string propertyName)
    {
        var parts = propertyName.Split(".");

        if (parts.Length == 2)
        {
            var property = Expression.Property(parameter, parts[0]);
            //TODO Recursively access nested properties?
            //     For now only allow one child.
            return Expression.Property(property, propertyName[(propertyName.IndexOf(".") + 1)..]);
        }

        return Expression.Property(parameter, propertyName);
    }

    private static MethodInfo EnumerableContains()
    {
        return typeof(Enumerable).GetMethods(BindingFlags.Static | BindingFlags.Public)
                                 .Single(m => m.Name == nameof(Enumerable.Contains) &&
                                              m.GetParameters().Length == 2);
    }

    private static MethodInfo StringContains()
    {
        return typeof(string).GetMethod(nameof(string.Contains), new Type[] { typeof(string) });
    }

    private static MethodInfo StringStartsWith()
    {
        return typeof(string).GetMethod(nameof(string.StartsWith), new Type[] { typeof(string) });
    }

    private static MethodInfo StringEndsWith()
    {
        return typeof(string).GetMethod(nameof(string.EndsWith), new Type[] { typeof(string) });
    }

    ///////////////////////////////
    /// Filtering Old (Material Table)
    ///////////////////////////////

    public static bool? ParseBooleanFilter(string value)
    {
        bool? result = null;

        if (!string.IsNullOrWhiteSpace(value))
        {
            if (bool.TryParse(value, out bool boolValue))
            {
                result = boolValue;
            }

            if (!result.HasValue)
            {
                if (value.ToLower() == "unchecked")
                {
                    result = false;
                }
                else if (value.ToLower() == "checked")
                {
                    result = true;
                }
            }
        }

        return result;
    }

    public static DateTime? ParseDateFilter(string value)
    {
        DateTime? result = null;

        if (!string.IsNullOrWhiteSpace(value))
        {
            if (DateTime.TryParse(value.Substring(4, 11), out DateTime val))
            {
                result = val;
            }
        }
        return result;
    }

    /// <summary>
    /// Transforms a pipe-delimeted filter string into a stringed dictionary.
    /// </summary>
    /// <param name="filters">A pipe-delimited filter string. E.g. 'code|1|name|a'</param>
    /// <returns>A dictionary in which each key is the filter name and the value is the filter value.</returns>
    public static Dictionary<string, string> TransformPipeDelimetedFilters(string filters)
    {
        var result = new Dictionary<string, string>();

        var arr = filters.Split("|");
        var length = arr.Length;
        for (int i = 0; i < length; i += 2)
        {
            var field = arr[i];
            var value = string.Empty;
            if ((i + 1) <= length - 1)
            {
                value = arr[i + 1];
            }

            if (!string.IsNullOrWhiteSpace(field) && !string.IsNullOrWhiteSpace(value))
            {
                result.Add(field, value);
            };
        }

        return result;
    }

    /// <summary>
    /// Transforms a comma separated filter value into a list of integers.
    /// </summary>
    /// <param name="filters">A comma separated filter value. E.g.  '1,2,3'</param>
    /// <returns>A list in which each key is the filter name and the value is the filter value.</returns>
    public static List<int> TransformCommaSeparatedFilter(string value)
    {
        if (!string.IsNullOrWhiteSpace(value))
        {
            return value.Split(",").Select(e => Convert.ToInt32(e)).ToList();
        }
        return new List<int>();
    }

    public static string WildcardFilter(string value)
    {
        if (!string.IsNullOrWhiteSpace(value))
        {
            return $"%{value}%";
        }
        return String.Empty;
    }

    private const string ASC = "ASC";
    private const string DESC = "DESC";

    public static string GetOrderDirection(string orderDirection)
    {
        if (!string.IsNullOrWhiteSpace(orderDirection) && orderDirection == DESC)
        {
            return DESC;
        }
        return ASC;
    }

    public static bool IsAsc(string orderDirection)
    {
        if (!string.IsNullOrWhiteSpace(orderDirection) && orderDirection == DESC)
        {
            return false;
        }
        return true;
    }

}
