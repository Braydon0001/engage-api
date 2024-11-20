namespace Engage.Application.Services.ProjectCampaigns.Queries;

public class ProjectCampaignPaginatedOptionQuery : PaginatedQuery, IRequest<List<ProjectCampaignOption>>
{
}

public record ProjectCampaignPaginatedOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectCampaignPaginatedOptionQuery, List<ProjectCampaignOption>>
{
    public async Task<List<ProjectCampaignOption>> Handle(ProjectCampaignPaginatedOptionQuery query, CancellationToken cancellationToken)
    {
        var props = ProjectCampaignPaginationProps.Create();

        var queryable = Context.ProjectCampaigns.AsQueryable().AsNoTracking();

        #region Custom Sort 
        if (query.SortModel.IsNullOrEmpty())
        {
            queryable = queryable.OrderByDescending(e => e.ProjectCampaignId);
        }
        #endregion

        return await queryable.Filter(query, props)
                              .Sort(query, props)
                              .Skip(query.StartRow)
                              .Take(query.PageSize)
                              .ProjectTo<ProjectCampaignOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}


