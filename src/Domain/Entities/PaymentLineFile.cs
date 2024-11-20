namespace Engage.Domain.Entities;

public class PaymentLineFile : BaseAuditableEntity
{
    public int PaymentLineFileId { get; set; }
    public int PaymentLineId { get; set; }
    public int PaymentLineFileTypeId { get; set; }
    public List<JsonFile> Files { get; set; }

    // Navigation Properties

    public PaymentLine PaymentLine { get; set; }
    public PaymentLineFileType PaymentLineFileType { get; set; }
}