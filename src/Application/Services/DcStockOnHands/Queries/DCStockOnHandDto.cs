namespace Engage.Application.Services.DCStockOnHands.Queries;

public class DCStockOnHandDto : IMapFrom<DCStockOnHand>
{
    public int Id { get; init; }
    public int DCProductId { get; init; }
    public string DCProductName { get; init; }
    public float OnOrderQuantity { get; init; }
    public DateTime StockDate { get; init; }
    public float Value { get; init; }
    public string Note { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<DCStockOnHand, DCStockOnHandDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.DCStockOnHandId));
    }
}
