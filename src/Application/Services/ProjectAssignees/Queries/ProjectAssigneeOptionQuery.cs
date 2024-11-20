namespace Engage.Application.Services.ProjectAssignees.Queries;

public class ProjectAssigneeOptionQuery : IRequest<List<ProjectAssigneeOption>>
{ 

}

public record ProjectAssigneeOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectAssigneeOptionQuery, List<ProjectAssigneeOption>>
{
    public async Task<List<ProjectAssigneeOption>> Handle(ProjectAssigneeOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProjectAssignees.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.ProjectAssigneeId)
                              .ProjectTo<ProjectAssigneeOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}