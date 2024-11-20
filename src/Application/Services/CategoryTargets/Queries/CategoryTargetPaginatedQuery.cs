namespace Engage.Application.Services.CategoryTargets.Queries;

public class CategoryTargetPaginatedQuery : PaginatedQuery, IRequest<List<CategoryTargetDto>>
{
}

public record CategoryTargetPaginatedHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CategoryTargetPaginatedQuery, List<CategoryTargetDto>>
{
    public async Task<List<CategoryTargetDto>> Handle(CategoryTargetPaginatedQuery query, CancellationToken cancellationToken)
    {
        var props = CategoryTargetPaginationProps.Create();

        var queryable = Context.CategoryTargets.AsQueryable().AsNoTracking();

        return await queryable.Filter(query, props)
                              .Sort(query, props)
                              .Skip(query.StartRow)
                              .Take(query.PageSize)
                              .ProjectTo<CategoryTargetDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}


