namespace Engage.Application.Services.ProjectCampaigns.Queries;

public class ProjectCampaignVm : IMapFrom<ProjectCampaign>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public List<JsonText> Note { get; init; }
    public OptionDto EngageRegionId { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public List<JsonFile> Files { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectCampaign, ProjectCampaignVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectCampaignId))
               .ForMember(d => d.EngageRegionId, opt => opt.MapFrom(s => s.EngageRegionId.HasValue ? new OptionDto(s.EngageRegionId.Value, s.EngageRegion.Name) : null));
    }
}
