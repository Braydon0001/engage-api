namespace Engage.Application.Services.ProductPrices.Queries;

public class ProductPriceDto : IMapFrom<ProductPrice>
{
    public int Id { get; init; }
    public int ProductId { get; init; }
    public DateTime StartDate { get; init; }
    public decimal Price { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductPrice, ProductPriceDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductPriceId));
    }
}
