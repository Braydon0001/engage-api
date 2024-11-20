namespace Engage.Application.Services.ProjectTaskPriorities.Queries;

public class ProjectTaskPriorityOptionQuery : IRequest<List<ProjectTaskPriorityOption>>
{ 

}

public record ProjectTaskPriorityOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectTaskPriorityOptionQuery, List<ProjectTaskPriorityOption>>
{
    public async Task<List<ProjectTaskPriorityOption>> Handle(ProjectTaskPriorityOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProjectTaskPriorities.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.ProjectTaskPriorityId)
                              .ProjectTo<ProjectTaskPriorityOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}