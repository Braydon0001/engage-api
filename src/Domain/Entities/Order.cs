namespace Engage.Domain.Entities;

public class Order : BaseAuditableEntity
{
    public Order()
    {
        OrderSkus = new HashSet<OrderSku>();
        OrderEngageDepartments = new HashSet<OrderEngageDepartment>();
    }
    public int OrderId { get; set; }
    public int OrderTypeId { get; set; }
    public int OrderStatusId { get; set; }
    public int StoreId { get; set; }
    public int DistributionCenterId { get; set; }
    public int? SupplierId { get; set; }
    public int? OrderTemplateId { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime? DeliveryDate { get; set; }
    public DateTime? SubmittedDate { get; set; }
    public string ProcessedBy { get; set; }
    public DateTime? ProcessedDate { get; set; }
    public string DCAccountNo { get; set; }
    public string OrderNo { get; set; }
    public string OrderReference { get; set; }
    public string Comment { get; set; }
    public string Note { get; set; }
    public string VATNumber { get; set; }
    public string AccountNumber { get; set; }
    public string Email { get; set; }
    public string Contact { get; set; }
    public string Address { get; set; }
    public string EmailedTo { get; set; }
    public List<JsonFile> Files { get; set; }

    // Navigation Properties
    public OrderType OrderType { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public Store Store { get; set; }
    public DistributionCenter DistributionCenter { get; set; }
    public Supplier Supplier { get; set; }
    public OrderTemplate OrderTemplate { get; set; }
    public ICollection<OrderSku> OrderSkus { get; set; }
    public ICollection<OrderEngageDepartment> OrderEngageDepartments { get; set; }
}
