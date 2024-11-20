using Engage.Application.Services.ProjectCampaigns.Queries;
using Engage.Application.Services.ProjectPriorities.Queries;
using Engage.Application.Services.ProjectStatuses.Queries;
using Engage.Application.Services.ProjectTypes.Queries;

namespace Engage.Application.Services.Projects.Queries;

public class ProjectVm : IMapFrom<Project>, IMapFrom<ProjectStore>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public OptionDto StoreId { get; set; }
    public ProjectTypeOption ProjectTypeId { get; init; }
    public ProjectStatusOption ProjectStatusId { get; init; }
    public ProjectPriorityOption ProjectPriorityId { get; init; }
    public ProjectCampaignOption ProjectCampaignId { get; init; }
    public OptionDto EngageRegionId { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public float? EstimatedHours { get; init; }
    public float? ActualHours { get; init; }
    //public List<JsonFile> Files { get; init; }
    //public List<JsonText> Note { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Project, ProjectVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectId))
               .ForMember(d => d.ProjectTypeId, opt => opt.MapFrom(s => s.ProjectType))
               .ForMember(d => d.ProjectStatusId, opt => opt.MapFrom(s => s.ProjectStatus))
               .ForMember(d => d.ProjectPriorityId, opt => opt.MapFrom(s => s.ProjectPriority))
               .ForMember(d => d.EngageRegionId, opt => opt.MapFrom(s => s.EngageRegionId.HasValue ? new OptionDto(s.EngageRegionId.Value, s.EngageRegion.Name) : null))
               .ForMember(d => d.ProjectCampaignId, opt => opt.MapFrom(s => s.ProjectCampaign));

        profile.CreateMap<ProjectStore, ProjectVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectId))
               .ForMember(d => d.ProjectTypeId, opt => opt.MapFrom(s => s.ProjectType))
               .ForMember(d => d.ProjectStatusId, opt => opt.MapFrom(s => s.ProjectStatus))
               .ForMember(d => d.ProjectPriorityId, opt => opt.MapFrom(s => s.ProjectPriority))
               //.ForMember(d => d.StoreId, opt => opt.MapFrom(s => new OptionDto(s.StoreId, s.Store.Name + " - " + s.EngageRegion.Name)))
               .ForMember(d => d.StoreId, opt => opt.Ignore())
               .ForMember(d => d.ProjectCampaignId, opt => opt.MapFrom(s => s.ProjectCampaign));
    }
}
