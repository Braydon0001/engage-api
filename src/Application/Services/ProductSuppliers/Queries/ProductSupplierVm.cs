// auto-generated
namespace Engage.Application.Services.ProductSuppliers.Queries;

public class ProductSupplierVm : IMapFrom<ProductSupplier>
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductSupplier, ProductSupplierVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductSupplierId));
    }
}
