using Engage.Application.Services.ProjectStakeholders.Queries;

namespace Engage.Application.Services.Projects.Queries;

public class ProjectStoreCreateVm
{
    public OptionDto ProjectOwnerId { get; set; }
    public List<ProjectStakeholderSearchOption> ProjectAssignedTo { get; set; }
    public List<ProjectStakeholderSearchOption> StakeholderIds { get; set; }
    public List<OptionDto> EngageBrandIds { get; set; }
}
