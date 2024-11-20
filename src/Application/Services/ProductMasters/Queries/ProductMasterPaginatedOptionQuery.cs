// auto-generated
namespace Engage.Application.Services.ProductMasters.Queries;

public class ProductMasterPaginatedOptionQuery : PaginatedQuery, IRequest<List<ProductMasterOption>>
{

}

public record ProductMasterPaginatedOptionListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProductMasterPaginatedOptionQuery, List<ProductMasterOption>>
{
    public async Task<List<ProductMasterOption>> Handle(ProductMasterPaginatedOptionQuery query, CancellationToken cancellationToken)
    {
        var props = ProductMasterPaginationProps.Create();

        var queryable = Context.ProductMasters.AsQueryable().AsNoTracking();

        if (query.SortModel.IsNullOrEmpty())
        {
            queryable = queryable.OrderBy(e => e.Name);
        }

        var entities = await queryable.Filter(query, props)
                                      .Sort(query, props)
                                      .Skip(query.StartRow)
                                      .Take(query.PageSize)
                                      .ProjectTo<ProductMasterOption>(Mapper.ConfigurationProvider)
                                      .ToListAsync(cancellationToken);

        return entities;
    }
}


