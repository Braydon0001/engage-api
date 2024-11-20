namespace Engage.Domain.Entities;

public class ProjectStatus : BaseAuditableEntity
{
    public int ProjectStatusId { get; set; }
    public string Name { get; set; }
}