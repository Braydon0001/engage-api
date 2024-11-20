namespace Engage.Application.Services.ProjectTaskStatuses.Queries;

public class ProjectTaskStatusListQuery : IRequest<List<ProjectTaskStatusDto>>
{

}

public record ProjectTaskStatusListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectTaskStatusListQuery, List<ProjectTaskStatusDto>>
{
    public async Task<List<ProjectTaskStatusDto>> Handle(ProjectTaskStatusListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProjectTaskStatuses.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.ProjectTaskStatusId)
                              .ProjectTo<ProjectTaskStatusDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}