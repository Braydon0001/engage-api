using Engage.Application.Services.DCProducts.Models;

namespace Engage.Application.Services.DCProducts.Queries;

public record DCProductPaginatedQuery(PaginatedQuery Query) : IRequest<ListResult<DCProductListDto>>
{
}

public record DCProductPaginatedHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<DCProductPaginatedQuery, ListResult<DCProductListDto>>
{
    public async Task<ListResult<DCProductListDto>> Handle(DCProductPaginatedQuery query, CancellationToken cancellationToken)
    {
        var paginationModels = CreatePaginationProps();

        var queryable = Context.DCProducts.AsQueryable().AsNoTracking();

        var entities = await queryable.Filter(query.Query, paginationModels)
                                      .Sort(query.Query, paginationModels)
                                      .SkipQuery(query.Query)
                                      .TakeQuery(query.Query)
                                      .ProjectTo<DCProductListDto>(Mapper.ConfigurationProvider)
                                      .ToListAsync(cancellationToken);

        return new(entities);
    }

    private static Dictionary<string, PaginationProperty> CreatePaginationProps()
    {
        return new Dictionary<string, PaginationProperty> {

            { "id", new PaginationProperty("DCProductId") },
            { "code", new PaginationProperty("Code") },
            { "name", new PaginationProperty("Name") },
        };
    }
}
