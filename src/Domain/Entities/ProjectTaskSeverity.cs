namespace Engage.Domain.Entities;

public class ProjectTaskSeverity : BaseAuditableEntity
{
    public int ProjectTaskSeverityId { get; set; }
    public string Name { get; set; }
}