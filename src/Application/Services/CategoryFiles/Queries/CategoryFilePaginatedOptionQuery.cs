namespace Engage.Application.Services.CategoryFiles.Queries;

public class CategoryFilePaginatedOptionQuery : PaginatedQuery, IRequest<List<CategoryFileOption>>
{
}

public record CategoryFilePaginatedOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CategoryFilePaginatedOptionQuery, List<CategoryFileOption>>
{
    public async Task<List<CategoryFileOption>> Handle(CategoryFilePaginatedOptionQuery query, CancellationToken cancellationToken)
    {
        var props = CategoryFilePaginationProps.Create();

        var queryable = Context.CategoryFiles.AsQueryable().AsNoTracking();

        return await queryable.Filter(query, props)
                              .Sort(query, props)
                              .Skip(query.StartRow)
                              .Take(query.PageSize)
                              .ProjectTo<CategoryFileOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }    
}


