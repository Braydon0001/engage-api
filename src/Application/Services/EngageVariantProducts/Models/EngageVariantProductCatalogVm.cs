namespace Engage.Application.Services.EngageVariantProducts.Models;

public class EngageVariantProductCatalogVm : IMapFrom<EngageVariantProduct>
{
    public int Id { get; set; }
    public int EngageMasterProductId { get; set; }
    public int UnitTypeId { get; set; }
    public string UnitTypeName { get; set; }
    public int SupplierId { get; set; }
    public string SupplierName { get; set; }
    public int VatId { get; set; }
    public bool IsDropShipment { get; set; }
    public string VatCode { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public float Size { get; set; }
    public float PackSize { get; set; }
    public string EANNumber { get; set; }
    public string UnitBarcode { get; set; }
    public string CaseBarcode { get; set; }
    public string ShrinkBarcode { get; set; }
    public bool Disabled { get; set; }
    public List<JsonFile> Files { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EngageVariantProduct, EngageVariantProductCatalogVm>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EngageVariantProductId))
            .ForMember(d => d.SupplierName, opt => opt.MapFrom(d => d.EngageMasterProduct.Supplier.Name))
            .ForMember(d => d.UnitTypeName, opt => opt.MapFrom(d => d.UnitType.Name))
            .ForMember(d => d.VatCode, opt => opt.MapFrom(d => d.EngageMasterProduct.Vat.Code));
    }
}
