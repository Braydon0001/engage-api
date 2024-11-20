namespace Engage.Application.Services.ProjectStatuses.Queries;

public record ProjectStatusVmQuery(int Id) : IRequest<ProjectStatusVm>;

public record ProjectStatusVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectStatusVmQuery, ProjectStatusVm>
{
    public async Task<ProjectStatusVm> Handle(ProjectStatusVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProjectStatuses.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.ProjectStatusId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<ProjectStatusVm>(entity);
    }
}