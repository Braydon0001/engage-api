namespace Engage.Domain.Entities;

public class PaymentPeriod : BaseAuditableEntity
{
    public int PaymentPeriodId { get; set; }
    public int PaymentYearId { get; set; }
    public string Name { get; set; }
    public int Number { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    // Navigation Properties

    public PaymentYear PaymentYear { get; set; }
}