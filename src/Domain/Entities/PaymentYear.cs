namespace Engage.Domain.Entities;

public class PaymentYear : BaseAuditableEntity
{
    public int PaymentYearId { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}