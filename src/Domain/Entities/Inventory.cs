namespace Engage.Domain.Entities;

public class Inventory : BaseAuditableEntity
{
    public int InventoryId { get; set; }
    public int InventoryGroupId { get; set; }
    public int InventoryStatusId { get; set; }
    public int InventoryUnitTypeId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string BarCode { get; set; }
    public List<JsonFile> Files { get; set; }

    // Navigation Properties

    public InventoryGroup InventoryGroup { get; set; }
    public InventoryStatus InventoryStatus { get; set; }
    public InventoryUnitType InventoryUnitType { get; set; }
}