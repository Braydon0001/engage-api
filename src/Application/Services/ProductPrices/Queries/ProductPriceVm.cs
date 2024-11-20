
using Engage.Application.Services.Products.Queries;

namespace Engage.Application.Services.ProductPrices.Queries;

public class ProductPriceVm : IMapFrom<ProductPrice>
{
    public int Id { get; init; }
    public ProductOption ProductId { get; init; }
    public DateTime StartDate { get; init; }
    public decimal Price { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductPrice, ProductPriceVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductPriceId))
               .ForMember(d => d.ProductId, opt => opt.MapFrom(s => s.Product));
    }
}
