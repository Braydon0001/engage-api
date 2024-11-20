using Engage.Application.Services.EngageMasterProducts.Models;

namespace Engage.Application.Services.EngageMasterProducts.Queries;

public record EngageMasterProductPaginatedQuery(PaginatedQuery Query) : IRequest<ListResult<EngageMasterProductListDto>>
{
}

public record EngageMasterProductPaginatedHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<EngageMasterProductPaginatedQuery, ListResult<EngageMasterProductListDto>>
{
    public async Task<ListResult<EngageMasterProductListDto>> Handle(EngageMasterProductPaginatedQuery query, CancellationToken cancellationToken)
    {
        var props = CreatePaginationProps();

        var queryable = Context.EngageMasterProducts.AsQueryable().AsNoTracking();

        var entities = await queryable.Include(e => e.EngageVariantProducts)
                                      .Filter(query.Query, props)
                                      .Sort(query.Query, props)
                                      .SkipQuery(query.Query)
                                      .TakeQuery(query.Query)
                                      .ProjectTo<EngageMasterProductListDto>(Mapper.ConfigurationProvider)
                                      .ToListAsync(cancellationToken);

        return new(entities);
    }

    private static Dictionary<string, PaginationProperty> CreatePaginationProps()
    {
        return new Dictionary<string, PaginationProperty> {

            { "id", new PaginationProperty("EngageMasterProductId") },
            { "code", new PaginationProperty("Code") },
            { "name", new PaginationProperty("Name") },
        };
    }
}
