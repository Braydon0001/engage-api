// auto-generated
namespace Engage.Domain.Entities;

public class ProductTransaction : BaseAuditableEntity
{
    public int ProductTransactionId { get; set; }
    public int ProductId { get; set; }
    public int ProductTransactionTypeId { get; set; }
    public int ProductTransactionStatusId { get; set; }
    public int ProductPeriodId { get; set; }
    public int ProductWarehouseId { get; set; }
    public int? EmployeeId { get; set; }
    public int? EngageRegionId { get; set; }
    public float Quantity { get; set; }
    public decimal Price { get; set; }
    public DateTime TransactionDate { get; set; }
    public string Note { get; set; }
    public List<JsonFile> Files { get; set; }

    // Navigation Properties

    public Product Product { get; set; }
    public ProductTransactionType ProductTransactionType { get; set; }
    public ProductTransactionStatus ProductTransactionStatus { get; set; }
    public ProductPeriod ProductPeriod { get; set; }
    public ProductWarehouse ProductWarehouse { get; set; }
    public Employee Employee { get; set; }
    public EngageRegion EngageRegion { get; set; }
}