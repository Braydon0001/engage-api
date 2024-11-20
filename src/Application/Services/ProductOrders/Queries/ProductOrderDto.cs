namespace Engage.Application.Services.ProductOrders.Queries;

public class ProductOrderDto : IMapFrom<ProductOrder>
{
    public int Id { get; init; }
    public string OrderNumber { get; init; }
    public int ProductOrderStatusId { get; init; }
    public string ProductOrderStatusName { get; init; }
    public int ProductOrderTypeId { get; init; }
    public string ProductOrderTypeName { get; init; }
    public int ProductWarehouseId { get; init; }
    public string ProductWarehouseName { get; init; }
    public int ProductPeriodId { get; init; }
    public string ProductPeriodName { get; init; }
    public int ProductSupplierId { get; init; }
    public string ProductSupplierName { get; init; }
    public string CreatedBy { get; init; }
    public DateTime OrderDate { get; init; }
    public List<JsonFile> Files { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductOrder, ProductOrderDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductOrderId));
    }
}

public class ProcessProductOrderDto
{
    public int Count { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public List<ProductOrderDto> Data { get; set; }
}