namespace Engage.Application.Services.DCStockOnHands.Queries;

public class DCStockOnHandVm : IMapFrom<DCStockOnHand>
{
    public int Id { get; init; }
    public OptionDto DcProductId { get; init; }
    public float OnOrderQuantity { get; init; }
    public DateTime StockDate { get; init; }
    public float Value { get; init; }
    public string Note { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<DCStockOnHand, DCStockOnHandVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.DCStockOnHandId))
               .ForMember(d => d.DcProductId, opt => opt.MapFrom(s => new OptionDto(s.DCProductId, s.DCProduct.Name)));
    }
}
