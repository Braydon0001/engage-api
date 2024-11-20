namespace Engage.Application.Services.OrderStagings.Queries;

public class OrderStagingPdfDto : IMapFrom<OrderStaging>
{
    public int Id { get; set; }
    public string Region { get; set; }
    public string StoreName { get; set; }
    public string AccountNumber { get; set; }
    public string OrderNumber { get; set; }
    public string OrderContactName { get; set; }
    public string OrderContactEmail { get; set; }
    public string VatNumber { get; set; }
    public string OrderDate { get; set; }
    public string Reference { get; set; }
    public Dictionary<string, List<OrderStagingSkuPdfDto>> Skus { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<OrderStaging, OrderStagingPdfDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.OrderStagingId))
               .ForMember(d => d.OrderDate, opt => opt.MapFrom(s => s.Date))
               .ForMember(d => d.Skus, opt => opt.Ignore());
    }
}

public class OrderStagingSkuPdfDto : IMapFrom<OrderStagingSku>
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public string Barcode { get; set; }
    public string UnitType { get; set; }
    public int Quantity { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<OrderStagingSku, OrderStagingSkuPdfDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.OrderStagingSkuId));
    }
}