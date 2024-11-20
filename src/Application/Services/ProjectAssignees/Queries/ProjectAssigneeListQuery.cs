namespace Engage.Application.Services.ProjectAssignees.Queries;

public class ProjectAssigneeListQuery : IRequest<List<ProjectAssigneeDto>>
{

}

public record ProjectAssigneeListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectAssigneeListQuery, List<ProjectAssigneeDto>>
{
    public async Task<List<ProjectAssigneeDto>> Handle(ProjectAssigneeListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProjectAssignees.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.ProjectAssigneeId)
                              .ProjectTo<ProjectAssigneeDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}