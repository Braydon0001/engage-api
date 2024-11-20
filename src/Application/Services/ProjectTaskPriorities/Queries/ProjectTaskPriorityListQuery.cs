namespace Engage.Application.Services.ProjectTaskPriorities.Queries;

public class ProjectTaskPriorityListQuery : IRequest<List<ProjectTaskPriorityDto>>
{

}

public record ProjectTaskPriorityListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectTaskPriorityListQuery, List<ProjectTaskPriorityDto>>
{
    public async Task<List<ProjectTaskPriorityDto>> Handle(ProjectTaskPriorityListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProjectTaskPriorities.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.ProjectTaskPriorityId)
                              .ProjectTo<ProjectTaskPriorityDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}