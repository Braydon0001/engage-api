namespace Engage.Application.Services.Orders.Models;

public class OrderSummaryPdfVm : IMapFrom<Order>
{
    public int Id { get; set; }
    public string StoreName { get; set; }
    public string DistributionCenterName { get; set; }
    public string OrderDate { get; set; }
    public string DeliveryDate { get; set; }
    public string OrderReference { get; set; }
    public string PlacedBy { get; set; }
    public string CreatedBy { get; set; }
    public OrderSummarySkusByQuantityTypePdfDto OrderSkus { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Order, OrderSummaryPdfVm>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.OrderId))
            .ForMember(d => d.OrderDate, opt => opt.MapFrom(s => s.OrderDate.ToShortDateString()))
            .ForMember(d => d.DeliveryDate, opt => opt.MapFrom(s => s.DeliveryDate.HasValue ? s.DeliveryDate.Value.ToShortDateString() : ""))
            .ForMember(d => d.OrderSkus, opt => opt.Ignore());
    }
}

public class OrderSummarySkusByQuantityTypePdfDto
{
    public Dictionary<string, List<OrderSummarySkusPdfVm>> ProductSkus { get; set; }
    public List<OrderSummarySkusPdfVm> FreeTextSkus { get; set; }
}

public class OrderSummarySkusPdfVm : IMapFrom<OrderSku>
{
    public int Id { get; set; }
    public string DCProductCode { get; set; }
    public string DCProductName { get; set; }
    public DateTime? DeliveryDate { get; set; }
    public int Quantity { get; set; }
    public string OrderQuantityTypeName { get; set; }
    public int OrderSkuTypeId { get; set; }
    public string OrderSkuTypeName { get; set; }
    public float DCProductSize { get; set; }
    public string DCProductUnitType { get; set; }
    public float DCProductPackSize { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<OrderSku, OrderSummarySkusPdfVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.OrderSkuId))
               .ForMember(d => d.DeliveryDate, opt => opt.MapFrom(s => s.DeliveryDate.HasValue ? s.DeliveryDate.Value.Date : s.Order.DeliveryDate.Value.Date))
               .ForMember(d => d.DCProductSize, opt => opt.MapFrom(s => s.DCProduct.Size))
               .ForMember(d => d.DCProductPackSize, opt => opt.MapFrom(s => s.DCProduct.PackSize))
               .ForMember(d => d.DCProductUnitType, opt => opt.MapFrom(s => s.DCProduct.UnitType.Name))
               .ForMember(d => d.DCProductCode, opt => opt.MapFrom(s => s.DCProduct.Code))
               .ForMember(d => d.DCProductName, opt => opt.MapFrom(s => s.DCProduct.Name))
               .ForMember(d => d.DCProductSize, opt => opt.MapFrom(s => s.DCProduct.Size));

    }

}