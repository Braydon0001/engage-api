namespace Engage.Domain.Entities;

public class ProjectTaskPriority : BaseAuditableEntity
{
    public int ProjectTaskPriorityId { get; set; }
    public string Name { get; set; }
}