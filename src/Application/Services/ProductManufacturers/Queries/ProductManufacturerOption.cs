// auto-generated
namespace Engage.Application.Services.ProductManufacturers.Queries;

public class ProductManufacturerOption : IMapFrom<ProductManufacturer>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductManufacturer, ProductManufacturerOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductManufacturerId));
    }
}