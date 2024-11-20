namespace Engage.Domain.Entities;

public class ProjectStatusHistory : BaseAuditableEntity
{
    public int ProjectStatusHistoryId { get; set; }
    public int ProjectId { get; set; }
    public int ProjectStatusId { get; set; }
    public string Reason { get; set; }

    // Navigation Properties

    public Project Project { get; set; }
    public ProjectStatus ProjectStatus { get; set; }
}