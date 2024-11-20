namespace Engage.Application.Services.SparSubProducts.Queries;

public class SparSubProductPaginatedQuery : PaginatedQuery, IRequest<List<SparSubProductDto>>
{
}

public record SparSubProductPaginatedHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SparSubProductPaginatedQuery, List<SparSubProductDto>>
{
    public async Task<List<SparSubProductDto>> Handle(SparSubProductPaginatedQuery query, CancellationToken cancellationToken)
    {
        var props = SparSubProductPaginationProps.Create();

        var queryable = Context.SparSubProducts.AsQueryable().AsNoTracking();

        return await queryable.Filter(query, props)
                              .Sort(query, props)
                              .Skip(query.StartRow)
                              .Take(query.PageSize)
                              .ProjectTo<SparSubProductDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}


