namespace Engage.Domain.Entities;

public class InventoryYear : BaseAuditableEntity
{
    public int InventoryYearId { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}