// auto-generated
namespace Engage.Domain.Entities;

public class InventoryTransactionType : BaseAuditableEntity
{
    public int InventoryTransactionTypeId { get; set; }
    public string Name { get; set; }
    public bool IsPositive { get; set; }
}