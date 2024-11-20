using System.Linq.Dynamic.Core;

namespace Engage.Application.Pagination.Extensions;

public static class PaginationExtensions
{
    public static IQueryable<TEntity> Paginate<TEntity>(this IQueryable<TEntity> queryable, PaginatedQuery query, Dictionary<string, PaginationProperty> props)
    {
        return queryable.Filter(query, props)
                        .Sort(query, props)
                        .SkipQuery(query)
                        .TakeQuery(query);
    }

    public static IQueryable<TEntity> Filter<TEntity>(this IQueryable<TEntity> queryable, PaginatedQuery query, Dictionary<string, PaginationProperty> props)
    {
        if (query.FilterModel != null)
        {
            foreach (var filter in query.FilterModel)
            {
                props.TryGetValue(filter.Key, out PaginationProperty paginationProperty);

                if (paginationProperty != null && !string.IsNullOrWhiteSpace(paginationProperty.Property))
                {
                    var filterType = filter.Value.FilterType.ToLower();
                    var type = filter.Value?.Type?.ToLower();
                    var filterValue = filter.Value?.Filter;
                    var property = paginationProperty.Property;

                    switch (filterType)
                    {
                        case "text":
                            switch (type)
                            {
                                case "equals":
                                    queryable = queryable.Where($"{property} == @0", filterValue);
                                    break;
                                case "notequal":
                                    queryable = queryable.Where($"!{property} != @0", filterValue);
                                    break;
                                case "contains":
                                    queryable = queryable.Where($"{property}.Contains(@0)", filterValue);
                                    break;
                                case "notcontains":
                                    queryable = queryable.Where($"!{property}.Contains(@0)", filterValue);
                                    break;
                            }
                            break;
                        case "number":
                            switch (type)
                            {
                                case "equals":
                                    queryable = queryable.Where($"{property} == @0", filterValue);
                                    break;
                                case "notequal":
                                    queryable = queryable.Where($"{property} != @0", filterValue);
                                    break;
                                case "lessthan":
                                    queryable = queryable.Where($"{property} < @0", filterValue);
                                    break;
                                case "lessthanorequal":
                                    queryable = queryable.Where($"{property} <= @0", filterValue);
                                    break;
                                case "greaterthan":
                                    queryable = queryable.Where($"{property} > @0", filterValue);
                                    break;
                                case "greaterthanorequal":
                                    queryable = queryable.Where($"{property} >= @0", filterValue);
                                    break;
                                case "inrange":
                                    var filterTo = filter.Value.FilterTo;
                                    queryable = queryable.Where($"{property} >= @0 && {property} <= @1", filterValue, filterTo);
                                    break;
                            }
                            break;
                        case "set":
                            var values = filter.Value.Values;
                            queryable = queryable.Where($"@0.Contains({property})", values);
                            break;
                        case "date":
                            var dateValue = filter.Value.DateFrom.Value;
                            switch (type)
                            {
                                case "equals":
                                    queryable = queryable.Where($"{property} == @0", dateValue.Date);
                                    break;
                                case "notequal":
                                    queryable = queryable.Where($"{property} != @0", dateValue.Date);
                                    break;
                                case "lessthan":
                                    queryable = queryable.Where($"{property} < @0", dateValue.Date);
                                    break;
                                case "lessthanorequal":
                                    queryable = queryable.Where($"{property} <= @0", dateValue.Date);
                                    break;
                                case "greaterthan":
                                    queryable = queryable.Where($"{property} > @0", dateValue.Date);
                                    break;
                                case "greaterthanorequal":
                                    queryable = queryable.Where($"{property} >= @0", dateValue.Date);
                                    break;
                                case "inrange":
                                    var dateTo = filter.Value.DateTo.Value;
                                    queryable = queryable.Where($"{property} >= @0 && {property} <= @1", dateValue.Date, dateTo.Date);
                                    break;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        return queryable;
    }

    public static IQueryable<TEntity> Sort<TEntity>(this IQueryable<TEntity> queryable, PaginatedQuery query, Dictionary<string, PaginationProperty> props)
    {
        if (query.SortModel.IsNotNullOrEmpty())
        {
            var sorts = new List<string>();
            foreach (var sort in query.SortModel)
            {
                props.TryGetValue(sort.ColId, out PaginationProperty paginationProperty);

                if (paginationProperty != null && paginationProperty.Property != null)
                {
                    var property = paginationProperty.SortProperty ?? paginationProperty.Property;
                    var order = sort.Sort == "asc" ? "" : " desc";

                    sorts.Add($"{property}{order}");
                }
            }

            if (sorts.Count > 0)
            {
                var orderBy = string.Join(", ", sorts.ToArray());
                queryable = queryable.OrderBy(orderBy);
            }
        }

        return queryable;
    }

    public static IQueryable<TEntity> SkipQuery<TEntity>(this IQueryable<TEntity> queryable, PaginatedQuery query)
    {
        return queryable.Skip(query.StartRow >= 0 ? query.StartRow : 0);
    }

    public static IQueryable<TEntity> TakeQuery<TEntity>(this IQueryable<TEntity> queryable, PaginatedQuery query)
    {
        return queryable.Take(query.PageSize > 0 ? query.PageSize : 100);
    }
}