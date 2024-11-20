namespace Engage.Application.Services.ProjectPriorities.Queries;

public class ProjectPriorityOptionQuery : IRequest<List<ProjectPriorityOption>>
{ 

}

public record ProjectPriorityOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectPriorityOptionQuery, List<ProjectPriorityOption>>
{
    public async Task<List<ProjectPriorityOption>> Handle(ProjectPriorityOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProjectPriorities.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.ProjectPriorityId)
                              .ProjectTo<ProjectPriorityOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}