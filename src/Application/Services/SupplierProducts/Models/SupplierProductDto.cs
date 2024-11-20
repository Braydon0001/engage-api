namespace Engage.Application.Services.SupplierProducts.Models;

public class SupplierProductDto : IMapFrom<SupplierProduct>
{
    public int SupplierId { get; set; }
    public string SupplierName { get; set; }
    public int EngageMasterProductId { get; set; }
    public string EngageMasterProductName { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierProduct, SupplierProductDto>()
             .ForMember(d => d.SupplierName, opts => opts.MapFrom(s => s.Supplier.Name))
             .ForMember(d => d.EngageMasterProductName, opts => opts.MapFrom(s => s.EngageMasterProduct.Name));
    }
}
