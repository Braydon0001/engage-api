// auto-generated
namespace Engage.Application.Services.ProductBrands.Queries;

public class ProductBrandOption : IMapFrom<ProductBrand>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductBrand, ProductBrandOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductBrandId));
    }
}