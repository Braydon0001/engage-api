namespace Engage.Application.Services.Projects.Queries;

public class ProjectPaginatedOptionQuery : PaginatedQuery, IRequest<List<ProjectOption>>
{
}

public record ProjectPaginatedOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectPaginatedOptionQuery, List<ProjectOption>>
{
    public async Task<List<ProjectOption>> Handle(ProjectPaginatedOptionQuery query, CancellationToken cancellationToken)
    {
        var props = ProjectPaginationProps.Create();

        var queryable = Context.Projects.AsQueryable().AsNoTracking();

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
                              .ProjectTo<ProjectOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}


