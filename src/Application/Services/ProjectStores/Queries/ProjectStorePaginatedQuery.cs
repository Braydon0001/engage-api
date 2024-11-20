using Engage.Application.Services.Projects.Queries;

namespace Engage.Application.Services.ProjectStores.Queries;

public class ProjectStorePaginatedQuery : PaginatedQuery, IRequest<List<ProjectDto>>
{
    public int? StoreId { get; set; }
    public int? UserId { get; set; }
    public string Search { get; set; }
}

public record ProjectStorePaginatedHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectStorePaginatedQuery, List<ProjectDto>>
{
    public async Task<List<ProjectDto>> Handle(ProjectStorePaginatedQuery query, CancellationToken cancellationToken)
    {
        var props = ProjectPaginationProps.Create();

        var queryable = Context.ProjectStores.AsQueryable().AsNoTracking();

        if (query.StoreId.HasValue)
        {
            queryable = queryable.Where(e => e.StoreId == query.StoreId);
        }

        if (query.UserId.HasValue)
        {
            var stakeholer = await Context.ProjectTaskProjectStakeholderUsers.FirstOrDefaultAsync(e => e.ProjectStakeholder.UserId == query.UserId, cancellationToken);

            var projectIds = await Context.ProjectTasks
                .Where(e => e.ProjectStakeholder.UserId == query.UserId)
                .Select(e => e.ProjectId)
                .ToListAsync(cancellationToken);

            if (projectIds.Any())
            {
                projectIds = projectIds.Distinct().ToList();
                queryable = queryable.Where(e => projectIds.Contains(e.ProjectId));
            }
        }

        #region Custom Sort 
        if (query.SortModel.IsNullOrEmpty())
        {
            queryable = queryable.OrderByDescending(e => e.ProjectId);
        }

        if (query.Search != null)
        {
            queryable = queryable.Where(x => (EF.Functions.Like(x.Name, $"%{query.Search}%")));
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