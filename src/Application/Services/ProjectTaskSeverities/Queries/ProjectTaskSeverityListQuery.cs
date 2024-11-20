namespace Engage.Application.Services.ProjectTaskSeverities.Queries;

public class ProjectTaskSeverityListQuery : IRequest<List<ProjectTaskSeverityDto>>
{

}

public record ProjectTaskSeverityListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectTaskSeverityListQuery, List<ProjectTaskSeverityDto>>
{
    public async Task<List<ProjectTaskSeverityDto>> Handle(ProjectTaskSeverityListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProjectTaskSeverities.AsQueryable().AsNoTracking();

        return await queryable.OrderBy(e => e.ProjectTaskSeverityId)
                              .ProjectTo<ProjectTaskSeverityDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}