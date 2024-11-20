namespace Engage.Domain.Entities;

public class FileContainer : BaseAuditableEntity
{
    public int FileContainerId { get; set; }
    public string Name { get; set; }
    public string ContainerName { get; set; }
    public bool PublicAccess { get; set; }
    public string FileNameStrategy { get; set; }

}
