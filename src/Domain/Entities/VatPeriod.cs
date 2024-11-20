namespace Engage.Domain.Entities;

public class VatPeriod : BaseAuditableEntity
{
    public int VatPeriodId { get; set; }
    public int VatId { get; set; }
    public DateTime StartDate { get; set; }
    public int Percent { get; set; }

    // Navigation Properties
    public Vat Vat { get; set; }
}
