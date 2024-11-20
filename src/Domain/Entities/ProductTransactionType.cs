// auto-generated
namespace Engage.Domain.Entities;

public class ProductTransactionType : BaseAuditableEntity
{
    public int ProductTransactionTypeId { get; set; }
    public string Name { get; set; }
    public bool IsPositive { get; set; }
}