namespace Engage.Application.Services.SparSubProducts.Queries;

public class SparSubProductPaginatedOptionQuery : PaginatedQuery, IRequest<List<SparSubProductOption>>
{
}

public record SparSubProductPaginatedOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SparSubProductPaginatedOptionQuery, List<SparSubProductOption>>
{
    public async Task<List<SparSubProductOption>> Handle(SparSubProductPaginatedOptionQuery query, CancellationToken cancellationToken)
    {
        var props = SparSubProductPaginationProps.Create();

        var queryable = Context.SparSubProducts.AsQueryable().AsNoTracking();

        return await queryable.Filter(query, props)
                              .Sort(query, props)
                              .Skip(query.StartRow)
                              .Take(query.PageSize)
                              .ProjectTo<SparSubProductOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }    
}


