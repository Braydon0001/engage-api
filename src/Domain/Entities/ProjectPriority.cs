namespace Engage.Domain.Entities;

public class ProjectPriority : BaseAuditableEntity
{
    public int ProjectPriorityId { get; set; }
    public string Name { get; set; }
    public bool IsEndDateRequired { get; set; }
}