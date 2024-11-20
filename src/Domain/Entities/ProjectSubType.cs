namespace Engage.Domain.Entities;

public class ProjectSubType : BaseAuditableEntity
{
    public int ProjectSubTypeId { get; set; }
    public int ProjectTypeId { get; set; }
    public string Name { get; set; }

    // Navigation Properties

    public ProjectType ProjectType { get; set; }
}