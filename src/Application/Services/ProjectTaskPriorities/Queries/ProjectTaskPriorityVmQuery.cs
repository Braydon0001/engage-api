namespace Engage.Application.Services.ProjectTaskPriorities.Queries;

public record ProjectTaskPriorityVmQuery(int Id) : IRequest<ProjectTaskPriorityVm>;

public record ProjectTaskPriorityVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectTaskPriorityVmQuery, ProjectTaskPriorityVm>
{
    public async Task<ProjectTaskPriorityVm> Handle(ProjectTaskPriorityVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProjectTaskPriorities.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.ProjectTaskPriorityId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<ProjectTaskPriorityVm>(entity);
    }
}