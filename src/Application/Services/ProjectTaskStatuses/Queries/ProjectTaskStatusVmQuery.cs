namespace Engage.Application.Services.ProjectTaskStatuses.Queries;

public record ProjectTaskStatusVmQuery(int Id) : IRequest<ProjectTaskStatusVm>;

public record ProjectTaskStatusVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectTaskStatusVmQuery, ProjectTaskStatusVm>
{
    public async Task<ProjectTaskStatusVm> Handle(ProjectTaskStatusVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProjectTaskStatuses.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.ProjectTaskStatusId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<ProjectTaskStatusVm>(entity);
    }
}