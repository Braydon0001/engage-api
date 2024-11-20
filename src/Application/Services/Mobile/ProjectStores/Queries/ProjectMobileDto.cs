namespace Engage.Application.Services.Mobile.ProjectStores.Queries;

public class ProjectMobileDto : IMapFrom<Project>, IMapFrom<ProjectStore>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public int ProjectTypeId { get; init; }
    public string ProjectTypeName { get; init; }
    public int ProjectStatusId { get; init; }
    public string ProjectStatusName { get; init; }
    public int ProjectPriorityId { get; set; }
    public string ProjectPriorityName { get; init; }
    public int? EngageRegionId { get; init; }
    public string EngageRegionName { get; init; }
    public int? ProjectCampaignId { get; init; }
    public string ProjectCampaignName { get; init; }

    public int StoreId { get; init; }
    public string StoreName { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public float? EstimatedHours { get; init; }
    public float? ActualHours { get; init; }
    public string ProjectTags { get; set; }
    public string ProjectUsers { get; set; }
    public List<JsonFile> Files { get; set; }
    public int OpenTasks { get; set; }
    public int AssignedTasks { get; set; }
    public int CompletedTasks { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Project, ProjectMobileDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectId))
               //.ForMember(d => d.ProjectTags, opt => opt.MapFrom(s => string.Join(", ", s.ProjectTags.Select(s => s.ProjectTag.Name))))
               .ForMember(d => d.ProjectUsers, opt => opt.MapFrom(s => string.Join(", ", s.ProjectUsers.Select(s => s.User.Email))))
               .ForMember(d => d.OpenTasks, opt => opt.MapFrom(s => s.ProjectTasks.Where(e => e.ProjectTaskStatusId == (int)ProjectTaskStatusId.Open).Count()))
               .ForMember(d => d.AssignedTasks, opt => opt.MapFrom(s => s.ProjectTasks.Where(e => e.ProjectTaskStatusId == (int)ProjectTaskStatusId.Assigned).Count()))
               .ForMember(d => d.CompletedTasks, opt => opt.MapFrom(s => s.ProjectTasks.Where(e => e.ProjectTaskStatusId == (int)ProjectTaskStatusId.Completed).Count()));



        profile.CreateMap<ProjectStore, ProjectMobileDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectId))
               //.ForMember(d => d.ProjectTags, opt => opt.MapFrom(s => string.Join(", ", s.ProjectTags.Select(s => s.ProjectTag.Name))))
               .ForMember(d => d.ProjectUsers, opt => opt.MapFrom(s => string.Join(", ", s.ProjectUsers.Select(s => s.User.Email))))
               .ForMember(d => d.OpenTasks, opt => opt.MapFrom(s => s.ProjectTasks.Where(e => e.ProjectTaskStatusId == (int)ProjectTaskStatusId.Open).Count()))
               .ForMember(d => d.AssignedTasks, opt => opt.MapFrom(s => s.ProjectTasks.Where(e => e.ProjectTaskStatusId == (int)ProjectTaskStatusId.Assigned).Count()))
               .ForMember(d => d.CompletedTasks, opt => opt.MapFrom(s => s.ProjectTasks.Where(e => e.ProjectTaskStatusId == (int)ProjectTaskStatusId.Completed).Count()));

    }
}
