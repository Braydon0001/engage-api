namespace Engage.Application.Services.ProductOrderLines.Queries;

public class ProductOrderLineDto : IMapFrom<ProductOrderLine>
{
    public int Id { get; set; }
    public int ProductOrderId { get; set; }
    public string ProductCode { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public int ProductOrderLineStatusId { get; set; }
    public string ProductOrderLineStatusName { get; set; }
    public int ProductOrderLineTypeId { get; set; }
    public string ProductOrderLineTypeName { get; set; }
    public double StockInWarehouse { get; set; }
    public decimal Amount { get; set; }
    public float Quantity { get; set; }
    public string Note { get; set; }
    public decimal TotalAmount { get; set; }
    public List<JsonFile> Files { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductOrderLine, ProductOrderLineDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductOrderLineId))
               .ForMember(d => d.ProductCode, opt => opt.MapFrom(s => s.Product.Code))
               .ForMember(d => d.TotalAmount, opt => opt.MapFrom(s => s.Amount * (decimal)s.Quantity));
    }
}
