namespace Engage.Domain.Entities;

public class ProjectTaskStatusHistory : BaseAuditableEntity
{
    public int ProjectTaskStatusHistoryId { get; set; }
    public int ProjectTaskId { get; set; }
    public int ProjectTaskStatusId { get; set; }
    public string Reason { get; set; }

    // Navigation Properties

    public ProjectTask ProjectTask { get; set; }
    public ProjectTaskStatus ProjectTaskStatus { get; set; }
}