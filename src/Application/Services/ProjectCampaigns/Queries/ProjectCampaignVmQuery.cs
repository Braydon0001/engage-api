namespace Engage.Application.Services.ProjectCampaigns.Queries;

public record ProjectCampaignVmQuery(int Id) : IRequest<ProjectCampaignVm>;

public record ProjectCampaignVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectCampaignVmQuery, ProjectCampaignVm>
{
    public async Task<ProjectCampaignVm> Handle(ProjectCampaignVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProjectCampaigns.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.EngageRegion);

        var entity = await queryable.SingleOrDefaultAsync(e => e.ProjectCampaignId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<ProjectCampaignVm>(entity);
    }
}