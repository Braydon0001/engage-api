namespace Engage.Application.Services.ProductPrices.Queries;

public class ProductPriceByProductVm : IMapFrom<ProductPrice>
{
    public int ProductId { get; set; }
    public decimal Price { get; set; }
    public bool IsAvailable { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductPrice, ProductPriceByProductVm>()
               .ForMember(d => d.ProductId, opt => opt.MapFrom(s => s.ProductId))
               .ForMember(d => d.IsAvailable, opt => opt.MapFrom(s => true));
    }
}
