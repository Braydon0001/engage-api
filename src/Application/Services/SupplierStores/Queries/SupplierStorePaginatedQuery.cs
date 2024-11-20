using Engage.Application.Services.SupplierStores.Models;

namespace Engage.Application.Services.SupplierStores.Queries;

public class SupplierStorePaginatedQuery : PaginatedQuery, IRequest<ListResult<SupplierStoreDto>>
{
    public int? SupplierId { get; set; }
}

public record SupplierStorePaginatedHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SupplierStorePaginatedQuery, ListResult<SupplierStoreDto>>
{
    public async Task<ListResult<SupplierStoreDto>> Handle(SupplierStorePaginatedQuery query, CancellationToken cancellationToken)
    {
        var props = SupplierStorePaginatedProps.Create();

        var queryable = Context.SupplierStores.AsQueryable().AsNoTracking();

        if (query.SupplierId.HasValue)
        {
            queryable = queryable.Where(e => e.SupplierId == query.SupplierId);
        }

        if (query.SortModel.IsNullOrEmpty())
        {
            queryable = queryable.OrderBy(e => e.EngageSubGroup.Name);
        }

        var entities = await queryable.Filter(query, props)
                                      .Sort(query, props)
                                      .SkipQuery(query)
                                      .TakeQuery(query)
                                      .ProjectTo<SupplierStoreDto>(Mapper.ConfigurationProvider)
                                      .ToListAsync(cancellationToken);

        return new(entities);
    }
}
