namespace Engage.Application.Services.OrderStagingSkus.Queries;

public class OrderStagingSkuDto : IMapFrom<OrderStagingSku>
{
    public int Id { get; init; }
    public int OrderStagingId { get; init; }
    public string ProductName { get; init; }
    public string Barcode { get; init; }
    public string UnitType { get; init; }
    public int Quantity { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<OrderStagingSku, OrderStagingSkuDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.OrderStagingSkuId));
    }
}
