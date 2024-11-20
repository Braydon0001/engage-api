namespace Engage.Application.Services.ProjectTasks.Queries;

public class ProjectTaskOptionQuery : IRequest<List<ProjectTaskOption>>
{ 

}

public record ProjectTaskOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectTaskOptionQuery, List<ProjectTaskOption>>
{
    public async Task<List<ProjectTaskOption>> Handle(ProjectTaskOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProjectTasks.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.ProjectTaskId)
                              .ProjectTo<ProjectTaskOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}