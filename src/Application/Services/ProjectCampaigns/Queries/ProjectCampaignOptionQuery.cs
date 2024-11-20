namespace Engage.Application.Services.ProjectCampaigns.Queries;

public class ProjectCampaignOptionQuery : IRequest<List<ProjectCampaignOption>>
{

}

public record ProjectCampaignOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectCampaignOptionQuery, List<ProjectCampaignOption>>
{
    public async Task<List<ProjectCampaignOption>> Handle(ProjectCampaignOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProjectCampaigns.AsQueryable().AsNoTracking();

        return await queryable.OrderBy(e => e.ProjectCampaignId)
                              .ProjectTo<ProjectCampaignOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}