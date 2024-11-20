// auto-generated
namespace Engage.Application.Services.ProductBrands.Queries;

public class ProductBrandVm : IMapFrom<ProductBrand>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string SparBrand { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductBrand, ProductBrandVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductBrandId));
    }
}
