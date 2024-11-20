// auto-generated
namespace Engage.Application.Services.ProductSuppliers.Queries;

public class ProductSupplierOption : IMapFrom<ProductSupplier>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductSupplier, ProductSupplierOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductSupplierId));
    }
}