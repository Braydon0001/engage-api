// auto-generated
namespace Engage.Application.Services.ProductVendors.Queries;

public class ProductVendorDto : IMapFrom<ProductVendor>
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductVendor, ProductVendorDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductVendorId));
    }
}
