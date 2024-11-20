namespace Engage.Application.Services.ProjectTaskAssignees.Queries;

public class ProjectTaskAssigneeOptionQuery : IRequest<List<ProjectTaskAssigneeOption>>
{ 

}

public record ProjectTaskAssigneeOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectTaskAssigneeOptionQuery, List<ProjectTaskAssigneeOption>>
{
    public async Task<List<ProjectTaskAssigneeOption>> Handle(ProjectTaskAssigneeOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProjectTaskAssignees.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.ProjectTaskAssigneeId)
                              .ProjectTo<ProjectTaskAssigneeOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}