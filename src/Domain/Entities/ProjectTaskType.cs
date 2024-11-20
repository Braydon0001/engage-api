namespace Engage.Domain.Entities;

public class ProjectTaskType : BaseAuditableEntity
{
    public int ProjectTaskTypeId { get; set; }
    public string Name { get; set; }
}