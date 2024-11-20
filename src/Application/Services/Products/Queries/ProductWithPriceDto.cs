namespace Engage.Application.Services.Products.Queries;

public class ProductWithPriceDto : IMapFrom<Product>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Product, ProductWithPriceDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductId))
               .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name + " - " + s.Code));
    }

}
