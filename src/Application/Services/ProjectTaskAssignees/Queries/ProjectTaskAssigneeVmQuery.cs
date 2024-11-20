namespace Engage.Application.Services.ProjectTaskAssignees.Queries;

public record ProjectTaskAssigneeVmQuery(int Id) : IRequest<ProjectTaskAssigneeVm>;

public record ProjectTaskAssigneeVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectTaskAssigneeVmQuery, ProjectTaskAssigneeVm>
{
    public async Task<ProjectTaskAssigneeVm> Handle(ProjectTaskAssigneeVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProjectTaskAssignees.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.ProjectTask)
                             .Include(e => e.Project)
                             .Include(e => e.ProjectTaskStatus);

        var entity = await queryable.SingleOrDefaultAsync(e => e.ProjectTaskAssigneeId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<ProjectTaskAssigneeVm>(entity);
    }
}