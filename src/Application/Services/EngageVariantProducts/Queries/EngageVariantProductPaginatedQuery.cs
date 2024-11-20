using Engage.Application.Services.EngageVariantProducts.Models;

namespace Engage.Application.Services.EngageVariantProducts.Queries;

public record EngageVariantProductPaginatedQuery(PaginatedQuery Query) : IRequest<ListResult<EngageVariantProductListDto>>
{
}

public record EngageVariantProductPaginatedHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<EngageVariantProductPaginatedQuery, ListResult<EngageVariantProductListDto>>
{

    public async Task<ListResult<EngageVariantProductListDto>> Handle(EngageVariantProductPaginatedQuery query, CancellationToken cancellationToken)
    {
        var paginationModels = CreatePaginationProps();

        var queryable = Context.EngageVariantProducts.AsQueryable().AsNoTracking();

        var entities = await queryable.Include(e => e.DCProducts)
                                      .Filter(query.Query, paginationModels)
                                      .Sort(query.Query, paginationModels)
                                      .SkipQuery(query.Query)
                                      .TakeQuery(query.Query)
                                      .ProjectTo<EngageVariantProductListDto>(Mapper.ConfigurationProvider)
                                      .ToListAsync(cancellationToken);

        return new(entities);
    }

    private static Dictionary<string, PaginationProperty> CreatePaginationProps()
    {
        return new Dictionary<string, PaginationProperty> {

            { "id", new PaginationProperty("EngageVariantProductId") },
            { "code", new PaginationProperty("Code") },
            { "name", new PaginationProperty("Name") },
        };
    }
}
