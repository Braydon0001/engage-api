namespace Engage.Application.Services.ProductMasters.Queries;

public class ProductMasterPaginatedQuery : PaginatedQuery, IRequest<ListResult<ProductMasterDto>>
{
}

public record ProductMasterPaginatedHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProductMasterPaginatedQuery, ListResult<ProductMasterDto>>
{

    public async Task<ListResult<ProductMasterDto>> Handle(ProductMasterPaginatedQuery query, CancellationToken cancellationToken)
    {
        var props = ProductMasterPaginationProps.Create();

        var queryable = Context.ProductMasters.AsQueryable().AsNoTracking();

        if (query.SortModel.IsNullOrEmpty())
        {
            queryable = queryable.OrderBy(e => e.Name);
        }

        var entities = await queryable.Filter(query, props)
                                      .Sort(query, props)
                                      .SkipQuery(query)
                                      .TakeQuery(query)
                                      .ProjectTo<ProductMasterDto>(Mapper.ConfigurationProvider)
                                      .ToListAsync(cancellationToken);

        return new(entities);
    }
}


