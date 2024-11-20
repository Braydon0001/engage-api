namespace Engage.Domain.Entities;

public class ProjectTaskStatus : BaseAuditableEntity
{
    public int ProjectTaskStatusId { get; set; }
    public string Name { get; set; }
}