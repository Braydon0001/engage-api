namespace Engage.Domain.Entities;

public class ProjectAssignee : BaseAuditableEntity
{
    public int ProjectAssigneeId { get; set; }
    public int ProjectId { get; set; }
    public int ProjectStakeholderId { get; set; }
    public int? ProjectStatusId { get; set; }

    // Navigation Properties

    public Project Project { get; set; }
    public ProjectStakeholder ProjectStakeholder { get; set; }
    public ProjectStatus ProjectStatus { get; set; }
}