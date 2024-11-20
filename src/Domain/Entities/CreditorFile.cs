namespace Engage.Domain.Entities;

public class CreditorFile : BaseAuditableEntity
{
    public int CreditorFileId { get; set; }
    public int CreditorId { get; set; }
    public int CreditorFileTypeId { get; set; }
    public List<JsonFile> Files { get; set; }

    // Navigation Properties

    public Creditor Creditor { get; set; }
    public CreditorFileType CreditorFileType { get; set; }
}