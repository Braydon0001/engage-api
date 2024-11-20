namespace Engage.Domain.Entities;

public class PaymentLineFileType : BaseAuditableEntity
{
    public int PaymentLineFileTypeId { get; set; }
    public string Name { get; set; }
}