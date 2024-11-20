namespace Engage.Domain.Entities;

public class FileUpload : BaseAuditableEntity
{
    public int FileUploadId { get; set; }
    public string FileName { get; set; }
    public DateTime? ImportDate { get; set; }
}
