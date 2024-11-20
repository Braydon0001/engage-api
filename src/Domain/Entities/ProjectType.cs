namespace Engage.Domain.Entities;

public class ProjectType : BaseAuditableEntity
{
    public int ProjectTypeId { get; set; }
    public string Name { get; set; }
    public bool IsDescriptionRequired { get; set; }
}