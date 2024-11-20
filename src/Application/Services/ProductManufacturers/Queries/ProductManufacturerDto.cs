// auto-generated
namespace Engage.Application.Services.ProductManufacturers.Queries;

public class ProductManufacturerDto : IMapFrom<ProductManufacturer>
{
    public int Id { get; set; }
    public int ProductSupplierId { get; set; }
    public string ProductSupplierName { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductManufacturer, ProductManufacturerDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductManufacturerId))
               .ForMember(d => d.ProductSupplierName, opt => opt.MapFrom(s => s.ProductSupplier.Code + " - " + s.ProductSupplier.Name));
    }
}
