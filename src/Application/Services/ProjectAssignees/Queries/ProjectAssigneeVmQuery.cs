namespace Engage.Application.Services.ProjectAssignees.Queries;

public record ProjectAssigneeVmQuery(int Id) : IRequest<ProjectAssigneeVm>;

public record ProjectAssigneeVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectAssigneeVmQuery, ProjectAssigneeVm>
{
    public async Task<ProjectAssigneeVm> Handle(ProjectAssigneeVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProjectAssignees.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.Project)
                             .Include(e => e.Project)
                             .Include(e => e.ProjectStatus);
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.ProjectAssigneeId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<ProjectAssigneeVm>(entity);
    }
}