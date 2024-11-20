namespace Engage.Application.Services.ProjectTaskSeverities.Queries;

public class ProjectTaskSeverityOptionQuery : IRequest<List<ProjectTaskSeverityOption>>
{

}

public record ProjectTaskSeverityOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectTaskSeverityOptionQuery, List<ProjectTaskSeverityOption>>
{
    public async Task<List<ProjectTaskSeverityOption>> Handle(ProjectTaskSeverityOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProjectTaskSeverities.AsQueryable().AsNoTracking();

        return await queryable.OrderBy(e => e.ProjectTaskSeverityId)
                              .ProjectTo<ProjectTaskSeverityOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}