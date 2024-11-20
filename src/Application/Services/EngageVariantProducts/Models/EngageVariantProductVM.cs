namespace Engage.Application.Services.EngageVariantProducts.Models;

public class EngageVariantProductVm : IMapFrom<EngageVariantProduct>
{
    public int Id { get; set; }
    public OptionDto EngageMasterProductId { get; set; }
    public OptionDto EngageMasterProductSupplierId { get; set; }
    public OptionDto EngageMasterProductVatId { get; set; }
    public OptionDto EngageMasterProductEngageBrandId { get; set; }
    public OptionDto EngageMasterProductGroupId { get; set; }
    public OptionDto EngageMasterProductSubGroupId { get; set; }
    public OptionDto EngageMasterProductCategoryId { get; set; }
    public OptionDto EngageMasterProductSubCategoryId { get; set; }
    public bool EngageMasterProductIsDropShipment { get; set; }
    public bool EngageMasterProductDisabled { get; set; }
    public string EngageMasterProductCode { get; set; }
    public OptionDto UnitTypeId { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public float Size { get; set; }
    public float PackSize { get; set; }
    public string EANNumber { get; set; }
    public string UnitBarcode { get; set; }
    public string CaseBarcode { get; set; }
    public string ShrinkBarcode { get; set; }
    public bool Disabled { get; set; }
    public bool IsMaster { set; get; }
    public List<JsonFile> Files { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EngageVariantProduct, EngageVariantProductVm>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EngageVariantProductId))
            .ForMember(d => d.EngageMasterProductId, opt => opt.MapFrom(d => new OptionDto(d.EngageMasterProductId, d.EngageMasterProduct.Name)))
            .ForMember(d => d.EngageMasterProductSupplierId, opt => opt.MapFrom(d => new OptionDto(d.EngageMasterProduct.SupplierId, d.EngageMasterProduct.Supplier.Name)))
            .ForMember(d => d.EngageMasterProductVatId, opt => opt.MapFrom(d => new OptionDto(d.EngageMasterProduct.VatId, d.EngageMasterProduct.Vat.Name)))
            .ForMember(d => d.EngageMasterProductEngageBrandId, opt => opt.MapFrom(d => new OptionDto(d.EngageMasterProduct.EngageBrandId, d.EngageMasterProduct.EngageBrand.Name)))
            .ForMember(d => d.EngageMasterProductGroupId, opt => opt.MapFrom(s => new OptionDto(s.EngageMasterProduct.EngageSubCategory.EngageCategory.EngageSubGroup.EngageGroupId, s.EngageMasterProduct.EngageSubCategory.EngageCategory.EngageSubGroup.EngageGroup.Name)))
            .ForMember(d => d.EngageMasterProductSubGroupId, opt => opt.MapFrom(s => new OptionDto(s.EngageMasterProduct.EngageSubCategory.EngageCategory.EngageSubGroupId, s.EngageMasterProduct.EngageSubCategory.EngageCategory.EngageSubGroup.Name)))
            .ForMember(d => d.EngageMasterProductCategoryId, opt => opt.MapFrom(s => new OptionDto(s.EngageMasterProduct.EngageSubCategory.EngageCategoryId, s.EngageMasterProduct.EngageSubCategory.EngageCategory.Name)))
            .ForMember(d => d.EngageMasterProductSubCategoryId, opt => opt.MapFrom(s => new OptionDto(s.EngageMasterProduct.EngageSubCategoryId, s.EngageMasterProduct.EngageSubCategory.Name)))
            .ForMember(d => d.EngageMasterProductDisabled, opt => opt.MapFrom(d => d.EngageMasterProduct.Disabled))
            .ForMember(d => d.EngageMasterProductIsDropShipment, opt => opt.MapFrom(d => d.EngageMasterProduct.IsDropShipment))
            .ForMember(d => d.EngageMasterProductCode, opt => opt.MapFrom(d => d.EngageMasterProduct.Code))
            .ForMember(d => d.UnitTypeId, opt => opt.MapFrom(d => new OptionDto(d.UnitTypeId, d.UnitType.Name)));
    }
}
