
using Engage.Application.Services.ProductOrders.Queries;
using Engage.Application.Services.Products.Queries;
using Engage.Application.Services.ProductOrderLineStatuses.Queries;
using Engage.Application.Services.ProductOrderLineTypes.Queries;

namespace Engage.Application.Services.ProductOrderLines.Queries;

public class ProductOrderLineVm : IMapFrom<ProductOrderLine>
{
    public int Id { get; init; }
    public ProductOrderOption ProductOrderId { get; init; }
    public ProductOption ProductId { get; init; }
    public ProductOrderLineStatusOption ProductOrderLineStatusId { get; init; }
    public ProductOrderLineTypeOption ProductOrderLineTypeId { get; init; }
    public decimal Amount { get; init; }
    public float Quantity { get; init; }
    public string Note { get; init; }
    public List<JsonFile> Files { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductOrderLine, ProductOrderLineVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductOrderLineId))
               .ForMember(d => d.ProductOrderId, opt => opt.MapFrom(s => s.ProductOrder))
               .ForMember(d => d.ProductId, opt => opt.MapFrom(s => s.Product))
               .ForMember(d => d.ProductOrderLineStatusId, opt => opt.MapFrom(s => s.ProductOrderLineStatus))
               .ForMember(d => d.ProductOrderLineTypeId, opt => opt.MapFrom(s => s.ProductOrderLineType));
    }
}
