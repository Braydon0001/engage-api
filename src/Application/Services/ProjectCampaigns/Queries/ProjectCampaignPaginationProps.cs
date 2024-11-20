namespace Engage.Application.Services.ProjectCampaigns.Queries;

public static class ProjectCampaignPaginationProps
{
    public static Dictionary<string, PaginationProperty> Create()
    {
        return new()
        {
            { "id", new("PojectId") },
            { "name", new("Name") },
            { "engageRegionName", new("EngageRegion.Name") },
        };
    }
}
