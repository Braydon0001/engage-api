namespace Engage.Application.Services.ProjectTasks.Queries;

public class ProjectTaskDto : IMapFrom<ProjectTask>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Note { get; init; }
    public int ProjectId { get; init; }
    public int ProjectTaskTypeId { get; init; }
    public string ProjectTaskTypeName { get; init; }
    public int ProjectTaskStatusId { get; init; }
    public string ProjectTaskStatusName { get; init; }
    public int ProjectTaskPriorityId { get; init; }
    public string ProjectTaskPriorityName { get; init; }
    //public int ProjectTaskSeverityId { get; init; }
    //public string ProjectTaskSeverityName { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public float? EstimatedHours { get; init; }
    public float? RemainingHours { get; init; }
    //public List<JsonFile> Files { get; init; }
    public int? UserId { get; set; }
    public int? ProjectStakeholderId { get; set; }
    public bool IsClosed { get; set; }
    public string UserName { get; set; }
    public string ProjectStakeholderName { get; set; }
    public string ProjectStakeholderNames { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectTask, ProjectTaskDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectTaskId))
               .ForMember(d => d.UserName, opt => opt.MapFrom(s => s.UserId.HasValue ? $"{s.User.FirstName} {s.User.LastName} - {s.User.Email}" : null))
               .ForMember(d => d.ProjectStakeholderName, opt => opt.MapFrom(s => s.ProjectStakeholderId.HasValue ? $"{s.ProjectStakeholder.User.FirstName} {s.ProjectStakeholder.User.LastName} - {s.ProjectStakeholder.User.Email}" : null))
               .ForMember(d => d.ProjectStakeholderNames, opt => opt.MapFrom(s => string.Join(", ", s.ProjectTaskProjectStakeholderUsers.Select(e => $"{e.ProjectStakeholder.User.FullName} - {e.ProjectStakeholder.User.Email}"))));
    }
}
