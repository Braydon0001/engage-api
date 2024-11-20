// auto-generated
namespace Engage.Domain.Entities;

public class InventoryTransaction : BaseAuditableEntity
{
    public int InventoryTransactionId { get; set; }
    public int InventoryTransactionTypeId { get; set; }
    public int InventoryTransactionStatusId { get; set; }
    public int InventoryId { get; set; }
    public int InventoryWarehouseId { get; set; }
    public float Quantity { get; set; }
    public DateTime TransactionDate { get; set; }
    public string Note { get; set; }
    public List<JsonFile> Files { get; set; }

    // Navigation Properties

    public InventoryTransactionType InventoryTransactionType { get; set; }
    public InventoryTransactionStatus InventoryTransactionStatus { get; set; }
    public Inventory Inventory { get; set; }
    public InventoryWarehouse InventoryWarehouse { get; set; }
}