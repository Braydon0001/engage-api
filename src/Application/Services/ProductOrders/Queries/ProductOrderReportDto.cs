namespace Engage.Application.Services.ProductOrders.Queries;

public class ProductOrderReportDto
{
    public string ProductWarehouseName { get; set; }
    public string FileName { get; set; }
    public List<ProdutOrderLineReportDto> Lines { get; set; }
}

public class ProdutOrderLineReportDto : IMapFrom<ProductOrderLine>
{
    //public int Id { get; set; }
    public int ProductOrderId { get; set; }
    public string OrderNumber { get; set; }
    public string OrderDate { get; set; }
    public string ProductOrderStatusName { get; set; }
    public string ProductOrderTypeName { get; set; }
    public string ProductCode { get; set; }
    public string ProductName { get; set; }
    public string ProductWarehouseName { get; set; }
    public string ProductWarehouseOutName { get; set; }
    public float Quantity { get; set; }
    public decimal Amount { get; set; }
    public string Note { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductOrderLine, ProdutOrderLineReportDto>()
            //.ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductOrderLineId))
            .ForMember(d => d.OrderNumber, opt => opt.MapFrom(s => s.ProductOrder.OrderNumber))
            .ForMember(d => d.OrderDate, opt => opt.MapFrom(s => s.ProductOrder.OrderDate.ToShortDateString()))
            .ForMember(d => d.ProductOrderStatusName, opt => opt.MapFrom(s => s.ProductOrder.ProductOrderStatus.Name))
            .ForMember(d => d.ProductOrderTypeName, opt => opt.MapFrom(s => s.ProductOrder.ProductOrderType.Name))
            .ForMember(d => d.ProductCode, opt => opt.MapFrom(s => s.Product.Code))
            .ForMember(d => d.ProductName, opt => opt.MapFrom(s => s.Product.Name))
            .ForMember(d => d.ProductWarehouseName, opt => opt.MapFrom(s => s.ProductOrder.ProductWarehouse.Name))
            .ForMember(d => d.ProductWarehouseOutName, opt => opt.MapFrom(s =>
                    s.ProductOrder.ProductWarehouseOutId != null ? s.ProductOrder.ProductWarehouseOut.Name : ""));
        //.ForMember(d => d.ProductWarehouseId, opt => opt.MapFrom(s => s.ProductOrder.ProductWarehouseId));
    }
}