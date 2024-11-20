namespace Engage.Application.Services.ProjectTaskSeverities.Queries;

public record ProjectTaskSeverityVmQuery(int Id) : IRequest<ProjectTaskSeverityVm>;

public record ProjectTaskSeverityVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectTaskSeverityVmQuery, ProjectTaskSeverityVm>
{
    public async Task<ProjectTaskSeverityVm> Handle(ProjectTaskSeverityVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProjectTaskSeverities.AsQueryable().AsNoTracking();

        var entity = await queryable.SingleOrDefaultAsync(e => e.ProjectTaskSeverityId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<ProjectTaskSeverityVm>(entity);
    }
}