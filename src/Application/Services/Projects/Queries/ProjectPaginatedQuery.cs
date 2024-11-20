namespace Engage.Application.Services.Projects.Queries;

public class ProjectPaginatedQuery : PaginatedQuery, IRequest<List<ProjectDto>>
{
}

public record ProjectPaginatedHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectPaginatedQuery, List<ProjectDto>>
{
    public async Task<List<ProjectDto>> Handle(ProjectPaginatedQuery query, CancellationToken cancellationToken)
    {
        var props = ProjectPaginationProps.Create();

        var queryable = Context.Projects.Where(e => EF.Property<string>(e, "Discriminator") == "Project")
                                        .AsQueryable()
                                        .AsNoTracking();

        #region Custom Sort 
        if (query.SortModel.IsNullOrEmpty())
        {
            queryable = queryable.OrderByDescending(e => e.ProjectId);
        }
        #endregion

        return await queryable.Filter(query, props)
                               .Sort(query, props)
                               .Skip(query.StartRow)
                               .Take(query.PageSize)
                               .ProjectTo<ProjectDto>(Mapper.ConfigurationProvider)
                               .ToListAsync(cancellationToken);
    }
}


