// auto-generated
namespace Engage.Application.Services.ProductBrands.Queries;

public class ProductBrandDto : IMapFrom<ProductBrand>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string SparBrand { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductBrand, ProductBrandDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductBrandId));
    }
}
