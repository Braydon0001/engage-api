// auto-generated
namespace Engage.Domain.Entities;

public class ProductPeriod : BaseAuditableEntity
{
    public int ProductPeriodId { get; set; }
    public int ProductYearId { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    // Navigation Properties

    public ProductYear ProductYear { get; set; }
}