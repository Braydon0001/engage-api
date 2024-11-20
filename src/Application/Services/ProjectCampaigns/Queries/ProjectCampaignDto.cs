namespace Engage.Application.Services.ProjectCampaigns.Queries;

public class ProjectCampaignDto : IMapFrom<ProjectCampaign>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public List<JsonText> Note { get; init; }
    public int? EngageRegionId { get; init; }
    public string EngageRegionName { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public List<JsonFile> Files { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectCampaign, ProjectCampaignDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectCampaignId));
    }
}
