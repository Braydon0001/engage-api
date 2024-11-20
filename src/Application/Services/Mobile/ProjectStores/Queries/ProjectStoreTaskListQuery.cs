using Engage.Application.Services.ProjectProjectTags.Queries;

namespace Engage.Application.Services.Mobile.ProjectStores.Queries;
public record ProjectStoreTaskListQuery(int? UserId, int StoreId) : IRequest<ListResult<ProjectWithTasks>>;

public record ProjectStoreTaskListHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator) : IRequestHandler<ProjectStoreTaskListQuery, ListResult<ProjectWithTasks>>
{
    public async Task<ListResult<ProjectWithTasks>> Handle(ProjectStoreTaskListQuery query, CancellationToken cancellationToken)
    {
        //get all the projects where the user is a stakeholder

        List<ProjectStakeholderUser> projectStakeholders = new List<ProjectStakeholderUser>();

        if (query.UserId != 0)
        {
            projectStakeholders = await Context.ProjectStakeholderUsers.Where(e => e.UserId == query.UserId)
                                                                        .ToListAsync(cancellationToken);
        }

        var projectStakeholderIds = projectStakeholders.Select(e => e.ProjectStakeholderId).ToList();

        var projectIds = projectStakeholders.Select(e => e.ProjectId).ToList();


        var queryable = Context.ProjectStores.AsQueryable().AsNoTracking();

        if (query.UserId != 0)
        {
            queryable = queryable.Where(e => projectIds.Contains(e.ProjectId));
        }

        queryable = queryable.Where(e => e.StoreId == query.StoreId && e.ProjectTasks.Count > 0);

        var projects = await queryable.Where(e => e.ProjectTasks.Any(t => t.ProjectStakeholderId.HasValue
                                                                          && projectStakeholderIds.Contains(t.ProjectStakeholderId.Value)))
                                      .ProjectTo<ProjectWithTasks>(Mapper.ConfigurationProvider)
                                      .ToListAsync(cancellationToken);

        if (query.UserId == 0)
        {
            var storeProjects = await queryable
                                    .ProjectTo<ProjectWithTasks>(Mapper.ConfigurationProvider)
                                    .ToListAsync(cancellationToken);

            var storeData = new ListResult<ProjectWithTasks>(storeProjects);

            return storeData;
        }

        foreach (var project in projects)
        {
            //remove the project tasks that the user is not a stakeholder of
            project.ProjectTasks = project.ProjectTasks.Where(e => e.ProjectStakeholderId.HasValue && projectStakeholderIds.Contains(e.ProjectStakeholderId.Value)).ToList();
            var tags = await Mediator.Send(new ProjectProjectTagListQuery { ProjectId = project.Id }, cancellationToken);
            //project.ProjectTags = string.Join(", ", tags.Select(s => s.Name + " - " + s.Type));
        }


        var data = new ListResult<ProjectWithTasks>(projects);

        return data;
    }
}
