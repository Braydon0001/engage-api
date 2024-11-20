namespace Engage.Application.Services.ProjectCategories.Queries;

public class ProjectCategoryOptionQuery : IRequest<List<ProjectCategoryOption>>
{
    public string Search { get; set; }
}

public record ProjectCategoryOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectCategoryOptionQuery, List<ProjectCategoryOption>>
{
    public async Task<List<ProjectCategoryOption>> Handle(ProjectCategoryOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProjectCategories.AsQueryable().AsNoTracking();

        if (query.Search.IsNotNullOrWhiteSpace())
        {
            queryable = queryable.Where(e => EF.Functions.Like(e.Name, $"%{query.Search}%"));
        }

        return await queryable.OrderBy(e => e.Name)
                              .Take(100)
                              .ProjectTo<ProjectCategoryOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}