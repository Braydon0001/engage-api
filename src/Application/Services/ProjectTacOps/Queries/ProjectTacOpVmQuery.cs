namespace Engage.Application.Services.ProjectTacOps.Queries;

public record ProjectTacOpVmQuery(int Id) : IRequest<ProjectTacOpVm>;

public record ProjectTacOpVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectTacOpVmQuery, ProjectTacOpVm>
{
    public async Task<ProjectTacOpVm> Handle(ProjectTacOpVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProjectTacOps.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.User)
                             .Include(e => e.ProjectTacOpRegions)
                             .ThenInclude(e => e.EngageRegion);

        var entity = await queryable.SingleOrDefaultAsync(e => e.ProjectTacOpId == query.Id, cancellationToken);

        if (entity == null)
        {
            return null;
        }

        var mappedEntity = entity == null ? null : Mapper.Map<ProjectTacOpVm>(entity);

        return mappedEntity;
    }
}