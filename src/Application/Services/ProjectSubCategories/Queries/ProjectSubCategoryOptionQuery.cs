namespace Engage.Application.Services.ProjectSubCategories.Queries;

public class ProjectSubCategoryOptionQuery : IRequest<List<ProjectSubCategoryOption>>
{
    public int? ProjectCategoryId { get; set; }
    public string Search { get; set; }
}

public record ProjectSubCategoryOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectSubCategoryOptionQuery, List<ProjectSubCategoryOption>>
{
    public async Task<List<ProjectSubCategoryOption>> Handle(ProjectSubCategoryOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProjectSubCategories.AsQueryable().AsNoTracking();

        if (query.ProjectCategoryId != null)
        {
            queryable = queryable.Where(e => e.ProjectCategoryId == query.ProjectCategoryId);
        }

        if (query.Search.IsNotNullOrWhiteSpace())
        {
            queryable = queryable.Where(e => EF.Functions.Like(e.Name, $"%{query.Search}%"));
        }

        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<ProjectSubCategoryOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}