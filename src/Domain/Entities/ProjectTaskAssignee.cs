namespace Engage.Domain.Entities;

public class ProjectTaskAssignee : BaseAuditableEntity
{
    public int ProjectTaskAssigneeId { get; set; }
    public int ProjectTaskId { get; set; }
    public int ProjectStakeholderId { get; set; }
    public int? ProjectTaskStatusId { get; set; }

    // Navigation Properties

    public ProjectTask ProjectTask { get; set; }
    public Project Project { get; set; }
    public ProjectTaskStatus ProjectTaskStatus { get; set; }
}