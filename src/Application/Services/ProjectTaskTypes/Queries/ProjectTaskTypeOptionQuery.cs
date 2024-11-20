namespace Engage.Application.Services.ProjectTaskTypes.Queries;

public class ProjectTaskTypeOptionQuery : IRequest<List<ProjectTaskTypeOption>>
{ 

}

public record ProjectTaskTypeOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectTaskTypeOptionQuery, List<ProjectTaskTypeOption>>
{
    public async Task<List<ProjectTaskTypeOption>> Handle(ProjectTaskTypeOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProjectTaskTypes.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.ProjectTaskTypeId)
                              .ProjectTo<ProjectTaskTypeOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}