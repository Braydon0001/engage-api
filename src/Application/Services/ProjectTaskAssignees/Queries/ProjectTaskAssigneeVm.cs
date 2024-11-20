
using Engage.Application.Services.Projects.Queries;
using Engage.Application.Services.ProjectTasks.Queries;
using Engage.Application.Services.ProjectTaskStatuses.Queries;

namespace Engage.Application.Services.ProjectTaskAssignees.Queries;

public class ProjectTaskAssigneeVm : IMapFrom<ProjectTaskAssignee>
{
    public int Id { get; init; }
    public ProjectTaskOption ProjectTaskId { get; init; }
    public ProjectOption ProjectStakeholderId { get; init; }
    public ProjectTaskStatusOption ProjectTaskStatusId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectTaskAssignee, ProjectTaskAssigneeVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectTaskAssigneeId))
               .ForMember(d => d.ProjectTaskId, opt => opt.MapFrom(s => s.ProjectTask))
               .ForMember(d => d.ProjectStakeholderId, opt => opt.MapFrom(s => s.Project))
               .ForMember(d => d.ProjectTaskStatusId, opt => opt.MapFrom(s => s.ProjectTaskStatus));
    }
}
