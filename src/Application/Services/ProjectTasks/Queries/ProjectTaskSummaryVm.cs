using Engage.Application.Services.ProjectTaskPriorities.Queries;
using Engage.Application.Services.ProjectTaskStatuses.Queries;

namespace Engage.Application.Services.ProjectTasks.Queries;

public class ProjectTaskSummaryVm : IMapFrom<ProjectTask>
{
    public int Id { get; init; }
    public string ProjectName { get; init; }
    public string Name { get; init; }
    public string Note { get; init; }
    public int ProjectId { get; init; }
    public ProjectTaskStatusOption ProjectTaskStatusId { get; init; }
    public ProjectTaskPriorityOption ProjectTaskPriorityId { get; init; }
    //public ProjectTaskSeverityOption ProjectTaskSeverityId { get; init; }
    public bool IsClosed { get; init; }
    public OptionDto UserId { get; set; }
    public OptionDto ProjectStakeholderId { get; set; }
    public List<OptionDto> ProjectStakeholderIds { get; set; }
    public List<JsonFile> Files { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectTask, ProjectTaskSummaryVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectTaskId))
               .ForMember(d => d.ProjectName, opt => opt.MapFrom(s => s.Project.Name))
               .ForMember(d => d.ProjectTaskStatusId, opt => opt.MapFrom(s => s.ProjectTaskStatus))
               .ForMember(d => d.ProjectTaskPriorityId, opt => opt.MapFrom(s => s.ProjectTaskPriority))
               //.ForMember(d => d.ProjectTaskSeverityId, opt => opt.MapFrom(s => s.ProjectTaskSeverity))
               .ForMember(d => d.UserId, opt => opt.MapFrom(s => s.UserId.HasValue ? new OptionDto(s.UserId.Value, s.User.FirstName + " " + s.User.LastName + " - " + s.User.Email) : null))
               .ForMember(d => d.ProjectStakeholderId, opt => opt.MapFrom(s => s.ProjectStakeholderId.HasValue ? new OptionDto(s.ProjectStakeholderId.Value, s.ProjectStakeholder.User.FirstName + " " + s.ProjectStakeholder.User.LastName + " - " + s.ProjectStakeholder.User.Email) : null))
               .ForMember(d => d.ProjectStakeholderIds, opt => opt.MapFrom(s => s.ProjectTaskProjectStakeholderUsers.Select(e => new OptionDto(e.ProjectStakeholderId, e.ProjectStakeholder.User.FullName + " - " + e.ProjectStakeholder.User.Email)).ToList()));
    }
}
