namespace Engage.Application.Services.OrderStagingSkus.Queries;

public class OrderStagingSkuOption : IMapFrom<OrderStagingSku>
{
    public int Id { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<OrderStagingSku, OrderStagingSkuOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.OrderStagingSkuId));
    }
}