// auto-generated
using Engage.Application.Services.ProductSuppliers.Queries;

namespace Engage.Application.Services.ProductManufacturers.Queries;

public class ProductManufacturerVm : IMapFrom<ProductManufacturer>
{
    public int Id { get; set; }
    public ProductSupplierOption ProductSupplierId { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductManufacturer, ProductManufacturerVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductManufacturerId))
               .ForMember(d => d.ProductSupplierId, opt => opt.MapFrom(s => s.ProductSupplier));
    }
}
