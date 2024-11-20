namespace Engage.Application.Services.Suppliers.Queries;

public class SupplierPaginatedOptionQuery : PaginatedQuery, IRequest<List<SupplierOption>>
{
};

public record SupplierPaginatedOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SupplierPaginatedOptionQuery, List<SupplierOption>>
{

    public async Task<List<SupplierOption>> Handle(SupplierPaginatedOptionQuery query, CancellationToken cancellationToken)
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
                                      .ProjectTo<SupplierOption>(Mapper.ConfigurationProvider)
                                      .ToListAsync(cancellationToken);

        return entities;
    }
}
