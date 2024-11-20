namespace Engage.Application.Services.ProjectTasks.Queries;

public class ProjectTaskListQuery : IRequest<List<ProjectTaskDto>>
{
    public int ProjectId { get; set; }
}

public record ProjectTaskListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectTaskListQuery, List<ProjectTaskDto>>
{
    public async Task<List<ProjectTaskDto>> Handle(ProjectTaskListQuery query, CancellationToken cancellationToken)
    {
        if (query.ProjectId < 1)
        {
            throw new Exception("Project not found");
        }

        var queryable = Context.ProjectTasks.Where(p => p.ProjectId == query.ProjectId)
                                            .AsQueryable()
                                            .AsNoTracking();

        return await queryable.OrderByDescending(e => e.ProjectTaskId)
                              .ProjectTo<ProjectTaskDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}