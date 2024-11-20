namespace Engage.Application.Services.Projects.Queries;

public record ProjectVmQuery(int Id) : IRequest<ProjectVm>;

public record ProjectVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectVmQuery, ProjectVm>
{
    public async Task<ProjectVm> Handle(ProjectVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.Projects.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.ProjectType)
                             .Include(e => e.ProjectStatus)
                             .Include(e => e.ProjectPriority)
                             .Include(e => e.EngageRegion)
                             .Include(e => e.ProjectCampaign);

        var entity = await queryable.SingleOrDefaultAsync(e => e.ProjectId == query.Id, cancellationToken);

        var mappedEntity = Mapper.Map<ProjectVm>(entity);

        return entity == null ? null : mappedEntity;
    }
}