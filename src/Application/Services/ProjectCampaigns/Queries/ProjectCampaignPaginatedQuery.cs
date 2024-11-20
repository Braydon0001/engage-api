namespace Engage.Application.Services.ProjectCampaigns.Queries;

public class ProjectCampaignPaginatedQuery : PaginatedQuery, IRequest<List<ProjectCampaignDto>>
{
}

public record ProjectCampaignPaginatedHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectCampaignPaginatedQuery, List<ProjectCampaignDto>>
{
    public async Task<List<ProjectCampaignDto>> Handle(ProjectCampaignPaginatedQuery query, CancellationToken cancellationToken)
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
                              .ProjectTo<ProjectCampaignDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}


