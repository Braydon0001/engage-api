namespace Engage.Application.Services.ProjectTaskTypes.Queries;

public record ProjectTaskTypeVmQuery(int Id) : IRequest<ProjectTaskTypeVm>;

public record ProjectTaskTypeVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectTaskTypeVmQuery, ProjectTaskTypeVm>
{
    public async Task<ProjectTaskTypeVm> Handle(ProjectTaskTypeVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProjectTaskTypes.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.ProjectTaskTypeId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<ProjectTaskTypeVm>(entity);
    }
}