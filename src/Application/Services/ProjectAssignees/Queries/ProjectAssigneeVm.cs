
using Engage.Application.Services.Projects.Queries;
using Engage.Application.Services.ProjectStatuses.Queries;

namespace Engage.Application.Services.ProjectAssignees.Queries;

public class ProjectAssigneeVm : IMapFrom<ProjectAssignee>
{
    public int Id { get; init; }
    public ProjectOption ProjectId { get; init; }
    public ProjectOption ProjectStakeholderId { get; init; }
    public ProjectStatusOption ProjectStatusId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectAssignee, ProjectAssigneeVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectAssigneeId))
               .ForMember(d => d.ProjectId, opt => opt.MapFrom(s => s.Project))
               .ForMember(d => d.ProjectStakeholderId, opt => opt.MapFrom(s => s.Project))
               .ForMember(d => d.ProjectStatusId, opt => opt.MapFrom(s => s.ProjectStatus));
    }
}
