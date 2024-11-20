using Engage.Application.Services.Stores.Models;

namespace Engage.Application.Services.Stores.Queries;

public class StorePaginatedQuery : PaginatedQuery, IRequest<ListResult<StoreListDto>>
{
}

public record StorePaginatedHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<StorePaginatedQuery, ListResult<StoreListDto>>
{

    public async Task<ListResult<StoreListDto>> Handle(StorePaginatedQuery query, CancellationToken cancellationToken)
    {
        var props = StorePaginationProps.Create();

        var queryable = Context.Stores.AsQueryable().AsNoTracking();

        #region Custom Filters
        if (query.FilterModel != null)
        {

            query.FilterModel.TryGetValue("accountNo", out FilterModel filterModel);
            if (filterModel != null && !string.IsNullOrWhiteSpace(filterModel.Filter))
            {
                var storeIds = await Context.DCAccounts.Where(e => e.AccountNumber.Contains(filterModel.Filter))
                                                       .Select(e => e.StoreId)
                                                       .ToListAsync(cancellationToken);
                queryable = queryable.Where(e => storeIds.Contains(e.StoreId));
            }
        }
        #endregion

        if (query.SortModel.IsNullOrEmpty())
        {
            queryable = queryable.OrderBy(e => e.Name);
        }

        var entities = await queryable.Filter(query, props)
                                      .Sort(query, props)
                                      .SkipQuery(query)
                                      .TakeQuery(query)
                                      .ProjectTo<StoreListDto>(Mapper.ConfigurationProvider)
                                      .ToListAsync(cancellationToken);

        return new(entities);
    }
}