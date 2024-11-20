namespace Engage.Application.Services.ProjectTaskStatuses.Queries;

public class ProjectTaskStatusOptionQuery : IRequest<List<ProjectTaskStatusOption>>
{ 

}

public record ProjectTaskStatusOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectTaskStatusOptionQuery, List<ProjectTaskStatusOption>>
{
    public async Task<List<ProjectTaskStatusOption>> Handle(ProjectTaskStatusOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProjectTaskStatuses.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.ProjectTaskStatusId)
                              .ProjectTo<ProjectTaskStatusOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}