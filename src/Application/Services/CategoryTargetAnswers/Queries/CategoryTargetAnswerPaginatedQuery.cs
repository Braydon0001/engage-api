namespace Engage.Application.Services.CategoryTargetAnswers.Queries;

public class CategoryTargetAnswerPaginatedQuery : PaginatedQuery, IRequest<List<CategoryTargetAnswerDto>>
{
}

public record CategoryTargetAnswerPaginatedHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CategoryTargetAnswerPaginatedQuery, List<CategoryTargetAnswerDto>>
{
    public async Task<List<CategoryTargetAnswerDto>> Handle(CategoryTargetAnswerPaginatedQuery query, CancellationToken cancellationToken)
    {
        var props = CategoryTargetAnswerPaginationProps.Create();

        var queryable = Context.CategoryTargetAnswers.AsQueryable().AsNoTracking();

        return await queryable.Filter(query, props)
                              .Sort(query, props)
                              .Skip(query.StartRow)
                              .Take(query.PageSize)
                              .ProjectTo<CategoryTargetAnswerDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}


