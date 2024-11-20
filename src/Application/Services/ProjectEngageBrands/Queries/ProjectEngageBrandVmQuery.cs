namespace Engage.Application.Services.ProjectEngageBrands.Queries;

public record ProjectEngageBrandVmQuery(int Id) : IRequest<ProjectEngageBrandVm>;

public record ProjectEngageBrandVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectEngageBrandVmQuery, ProjectEngageBrandVm>
{
    public async Task<ProjectEngageBrandVm> Handle(ProjectEngageBrandVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProjectEngageBrands.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.Project)
                             .Include(e => e.EngageBrand);
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.ProjectEngageBrandId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<ProjectEngageBrandVm>(entity);
    }
}