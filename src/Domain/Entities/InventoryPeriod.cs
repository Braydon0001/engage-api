// auto-generated
namespace Engage.Domain.Entities;

public class InventoryPeriod : BaseAuditableEntity
{
    public int InventoryPeriodId { get; set; }
    public int InventoryYearId { get; set; }
    public string Name { get; set; }
    public int Number { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    // Navigation Properties

    public InventoryYear InventoryYear { get; set; }
}