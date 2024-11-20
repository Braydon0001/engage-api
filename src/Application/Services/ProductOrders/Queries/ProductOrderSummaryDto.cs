using Engage.Application.Services.ProductOrderLines.Queries;

namespace Engage.Application.Services.ProductOrders.Queries;

public class ProductOrderSummaryDto : IMapFrom<ProductOrder>
{
    public int Id { get; set; }
    public string OrderNumber { get; set; }
    public decimal TotalAmount { get; set; }
    public int PriceMissingCount { get; set; }
    public List<ProductOrderLineDto> ProductOrderLines { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductOrder, ProductOrderSummaryDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductOrderId));
    }
}
