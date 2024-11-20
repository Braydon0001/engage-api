using Engage.Application.Services.ProjectTasks.Queries;

namespace Engage.Application.Services.Mobile.ProjectStores.Queries;

public class ProjectTasksQuery : IRequest<List<ProjectTaskDto>>
{
    public int UserId { get; set; }

}

public record ProjectTasksQueryHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectTasksQuery, List<ProjectTaskDto>>
{
    public async Task<List<ProjectTaskDto>> Handle(ProjectTasksQuery query, CancellationToken cancellationToken)
    {

        var queryable = Context.ProjectStores.AsQueryable().AsNoTracking();

        var stakeholer = await Context.ProjectTaskProjectStakeholderUsers.FirstOrDefaultAsync(e => e.ProjectStakeholder.UserId == query.UserId, cancellationToken);

        var tasks = await Context.ProjectTasks
            .Where(e => e.ProjectStakeholder.UserId == query.UserId)
            .Include(e => e.Project)
            .ProjectTo<ProjectTaskDto>(Mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return tasks;
    }
}
