namespace Engage.Application.Services.SparProducts.Queries;

public class SparProductPaginatedOptionQuery : PaginatedQuery, IRequest<List<SparProductOption>>
{
}

public record SparProductPaginatedOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SparProductPaginatedOptionQuery, List<SparProductOption>>
{
    public async Task<List<SparProductOption>> Handle(SparProductPaginatedOptionQuery query, CancellationToken cancellationToken)
    {
        var props = SparProductPaginationProps.Create();

        var queryable = Context.SparProducts.AsQueryable().AsNoTracking();

        return await queryable.Filter(query, props)
                              .Sort(query, props)
                              .Skip(query.StartRow)
                              .Take(query.PageSize)
                              .ProjectTo<SparProductOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }    
}


