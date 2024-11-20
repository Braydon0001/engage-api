namespace Engage.Application.Services.ProjectCampaigns.Queries;

public class ProjectCampaignOption : IMapFrom<ProjectCampaign>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectCampaign, ProjectCampaignOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectCampaignId));
    }
}