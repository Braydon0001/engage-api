using Engage.Application.Services.Suppliers.Models;

namespace Engage.Application.Services.Suppliers.Queries;

public class SupplierPaginatedQuery : PaginatedQuery, IRequest<ListResult<SupplierListDto>>
{
}

public record SupplierPaginatedHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SupplierPaginatedQuery, ListResult<SupplierListDto>>
{
    public async Task<ListResult<SupplierListDto>> Handle(SupplierPaginatedQuery query, CancellationToken cancellationToken)
    {
        var props = SupplierPaginationProps.Create();

        var queryable = Context.Suppliers.AsQueryable().AsNoTracking();

        if (query.SortModel.IsNullOrEmpty())
        {
            queryable = queryable.OrderBy(e => e.Name);
        }

        var entities = await queryable.Filter(query, props)
                                      .Sort(query, props)
                                      .SkipQuery(query)
                                      .TakeQuery(query)
                                      .ProjectTo<SupplierListDto>(Mapper.ConfigurationProvider)
                                      .ToListAsync(cancellationToken);

        return new(entities);
    }
}
