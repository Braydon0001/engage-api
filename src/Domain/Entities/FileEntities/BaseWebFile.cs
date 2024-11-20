namespace Engage.Domain.Entities.FileEntities;

public class BaseWebFile : BaseAuditableEntity
{
    public int FileTypeId { get; set; }
    public string FileName { get; set; }
    public string Url { get; set; }
    public string Metadata { get; set; }

    // Navigation Properties
    public FileType FileType { get; set; }
}
