namespace Engage.Application.Services.ProductOrderHistories.Queries;

public class ProductOrderHistoryOption : IMapFrom<ProductOrderHistory>
{
    public int Id { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductOrderHistory, ProductOrderHistoryOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductOrderHistoryId));
    }
}