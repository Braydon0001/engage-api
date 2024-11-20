namespace Engage.Domain.Entities;

public class FileType : BaseAuditableEntity
{
    public int FileTypeId { get; set; }
    public string Name { get; set; }
    public string Icon { get; set; }
    public bool CanView { get; set; }
    public bool IsUrl { get; set; }
}
