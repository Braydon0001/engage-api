namespace Engage.Application.Services.Shared.Queries;

public class GetQuery
{
    public string Search { get; set; }
    public string Filters { get; set; }
    public string OrderBy { get; set; }
    public string OrderDirection { get; set; }
    public int PageNo { get; set; }
    public int PageSize { get; set; }
    public void Deconstruct(
        out string search,
        out string filters,
        out string orderBy,
        out string orderDirection,
        out int pageNo,
        out int pageSize,
        out int skip)
    {
        search = Search;
        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.ToUpper();
        }

        filters = Filters;
        if (!string.IsNullOrWhiteSpace(filters))
        {
            filters = filters.ToUpper();
        }

        orderBy = OrderBy;
        if (!string.IsNullOrWhiteSpace(orderBy))
        {
            orderBy = orderBy.ToUpper();
        }

        orderDirection = OrderDirection;
        if (!string.IsNullOrWhiteSpace(orderDirection))
        {
            orderDirection = orderDirection.ToUpper();
        }

        pageNo = PageNo;
        if (pageNo == 0)
        {
            pageNo = 1;
        }

        pageSize = PageSize;
        if (pageSize == 0)
        {
            pageSize = 100;
        }

        skip = (pageNo - 1) * pageSize;
    }
}

public static class GetQueryExtensions
{
    /// <summary>
    /// Merges pagination values from a PaginatedQuery into a destination query.
    /// The destination query must inherit from PaginatedQuery.
    /// </summary>        
    public static TQuery Merge<TQuery>(this GetQuery query, TQuery destinationQuery) where TQuery : GetQuery
    {
        destinationQuery.Search = query.Search;
        destinationQuery.Filters = query.Filters;
        destinationQuery.OrderBy = query.OrderBy;
        destinationQuery.OrderDirection = query.OrderDirection;
        destinationQuery.PageNo = query.PageNo;
        destinationQuery.PageSize = query.PageSize;

        return destinationQuery;
    }
}
