namespace Engage.Domain.Entities;

public class CreditorFileType : BaseAuditableEntity
{
    public int CreditorFileTypeId { get; set; }
    public string Name { get; set; }
}