namespace Engage.Domain.Entities.FileEntities;

public class BaseFile : BaseAuditableEntity
{
    public int FileContainerId { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    public string Metadata { get; set; }

    // Navigation Properties

    public FileContainer FileContainer { get; set; }
}
