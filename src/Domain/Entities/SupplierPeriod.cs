namespace Engage.Domain.Entities;

public class SupplierPeriod : BaseAuditableEntity
{
    public int SupplierPeriodId { get; set; }
    public int SupplierYearId { get; set; }
    public int Number { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    // Navigation Properties
    public SupplierYear SupplierYear { get; set; }

}
