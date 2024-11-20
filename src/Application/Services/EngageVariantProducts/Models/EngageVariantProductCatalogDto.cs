namespace Engage.Application.Services.EngageVariantProducts.Models;

public class EngageVariantProductCatalogDto : IMapFrom<EngageVariantProduct>
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public int EngageMasterProductId { get; set; }
    public int UnitTypeId { get; set; }
    public string UnitTypeName { get; set; }
    public float Size { get; set; }
    public float PackSize { get; set; }
    public string EANNumber { get; set; }
    public string UnitBarcode { get; set; }
    public string CaseBarcode { get; set; }
    public string ShrinkBarcode { get; set; }
    public bool Disabled { get; set; }
    public bool Deleted { get; set; }
    public int NumberOfVariants { get; set; }
    public List<JsonFile> Files { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EngageVariantProduct, EngageVariantProductCatalogDto>()
            .ForMember(e => e.Id, opt => opt.MapFrom(d => d.EngageVariantProductId))
            .ForMember(e => e.NumberOfVariants, opt => opt.MapFrom(d => d.EngageMasterProduct.EngageVariantProducts.Count));
    }
}
