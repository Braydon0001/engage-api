namespace Engage.Application.Services.CategoryFiles.Queries;

public class CategoryFilePaginatedQuery : PaginatedQuery, IRequest<ListResult<CategoryFileDto>>
{

}

public record CategoryFilePaginatedHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CategoryFilePaginatedQuery, ListResult<CategoryFileDto>>
{
    public async Task<ListResult<CategoryFileDto>> Handle(CategoryFilePaginatedQuery query, CancellationToken cancellationToken)
    {
        var props = CategoryFilePaginationProps.Create();

        var queryable = Context.CategoryFiles.AsQueryable().AsNoTracking();

        if (query.SortModel.IsNullOrEmpty())
        {
            queryable = queryable.OrderByDescending(e => e.CategoryFileId);
        }

        var entities = await queryable.Filter(query, props)
                              .Sort(query, props)
                              .SkipQuery(query)
                              .TakeQuery(query)
                              .ProjectTo<CategoryFileDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);

        return new(entities);
    }
}


