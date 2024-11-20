namespace Engage.Application.Services.Orders.Commands;

public class OrderCommand : IMapTo<Order>
{
    public int OrderTypeId { get; set; }
    public int OrderStatusId { get; set; }
    public int StoreId { get; set; }
    public int DistributionCenterId { get; set; }
    public int? SupplierId { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime? DeliveryDate { get; set; }
    public DateTime? SubmittedDate { get; set; }
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
    public List<int> EngageDepartmentIds { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<OrderCommand, Order>();
    }
}
