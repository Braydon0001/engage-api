namespace Engage.Application.Services.ProjectTaskAssignees.Queries;

public class ProjectTaskAssigneeListQuery : IRequest<List<ProjectTaskAssigneeDto>>
{

}

public record ProjectTaskAssigneeListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectTaskAssigneeListQuery, List<ProjectTaskAssigneeDto>>
{
    public async Task<List<ProjectTaskAssigneeDto>> Handle(ProjectTaskAssigneeListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProjectTaskAssignees.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.ProjectTaskAssigneeId)
                              .ProjectTo<ProjectTaskAssigneeDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}