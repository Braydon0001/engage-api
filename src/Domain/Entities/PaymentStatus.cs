namespace Engage.Domain.Entities;

public class PaymentStatus : BaseAuditableEntity
{
    public int PaymentStatusId { get; set; }
    public string Name { get; set; }
}