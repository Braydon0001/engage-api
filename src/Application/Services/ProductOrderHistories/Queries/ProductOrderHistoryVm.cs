
using Engage.Application.Services.ProductOrders.Queries;
using Engage.Application.Services.ProductOrderStatuses.Queries;

namespace Engage.Application.Services.ProductOrderHistories.Queries;

public class ProductOrderHistoryVm : IMapFrom<ProductOrderHistory>
{
    public int Id { get; init; }
    public ProductOrderOption ProductOrderId { get; init; }
    public ProductOrderStatusOption ProductOrderStatusId { get; init; }
    public string Reason { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductOrderHistory, ProductOrderHistoryVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductOrderHistoryId))
               .ForMember(d => d.ProductOrderId, opt => opt.MapFrom(s => s.ProductOrder))
               .ForMember(d => d.ProductOrderStatusId, opt => opt.MapFrom(s => s.ProductOrderStatus));
    }
}
