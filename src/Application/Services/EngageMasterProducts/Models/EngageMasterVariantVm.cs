namespace Engage.Application.Services.EngageMasterProducts.Models;

public class EngageMasterVariantVm : IMapFrom<EngageMasterProduct>
{
    public int Id { get; set; }
    public int EngageMasterVariantId { get; set; }
    public OptionDto SupplierId { get; set; }
    public OptionDto ProductClassificationId { get; set; }
    public OptionDto EngageDepartmentId { get; set; }
    public OptionDto EngageBrandId { get; set; }
    public OptionDto VatId { get; set; }
    public OptionDto EngageGroupId { get; set; }
    public OptionDto EngageSubGroupId { get; set; }
    public OptionDto EngageCategoryId { get; set; }
    public OptionDto EngageSubCategoryId { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public bool IsVATProduct { get; set; }
    public bool IsDairyProduct { get; set; }
    public bool Disabled { get; set; }
    public bool IsAllSuppliersProduct { get; set; }
    public bool IsFreshProduct { get; set; }
    public bool IsDropShipment { get; set; }
    public string EANNumber { get; set; }
    public OptionDto UnitTypeId { get; set; }
    public float Size { get; set; }
    public float PackSize { get; set; }
    public List<OptionDto> EngageTagIds { get; set; }
    public List<JsonFile> Files { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EngageMasterProduct, EngageMasterVariantVm>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EngageMasterProductId))
            .ForMember(d => d.SupplierId, opt => opt.MapFrom(s => new OptionDto(s.SupplierId, s.Supplier.Name)))
            .ForMember(d => d.ProductClassificationId, opt => opt.MapFrom(s => new OptionDto(s.ProductClassificationId, s.ProductClassification.Name)))
            .ForMember(d => d.EngageDepartmentId, opt => opt.MapFrom(s => new OptionDto(s.EngageDepartmentId, s.EngageDepartment.Name)))
            .ForMember(d => d.EngageBrandId, opt => opt.MapFrom(s => new OptionDto(s.EngageBrandId, s.EngageBrand.Name)))
            .ForMember(d => d.VatId, opt => opt.MapFrom(s => new OptionDto(s.VatId, s.Vat.Name)))
            .ForMember(d => d.EngageGroupId, opt => opt.MapFrom(s => new OptionDto(s.EngageSubCategory.EngageCategory.EngageSubGroup.EngageGroupId, s.EngageSubCategory.EngageCategory.EngageSubGroup.EngageGroup.Name)))
            .ForMember(d => d.EngageSubGroupId, opt => opt.MapFrom(s => new OptionDto(s.EngageSubCategory.EngageCategory.EngageSubGroupId, s.EngageSubCategory.EngageCategory.EngageSubGroup.Name)))
            .ForMember(d => d.EngageCategoryId, opt => opt.MapFrom(s => new OptionDto(s.EngageSubCategory.EngageCategoryId, s.EngageSubCategory.EngageCategory.Name)))
            .ForMember(d => d.EngageSubCategoryId, opt => opt.MapFrom(s => new OptionDto(s.EngageSubCategoryId, s.EngageSubCategory.Name)))
            .ForMember(d => d.EngageTagIds, opt => opt.MapFrom(s => s.EngageProductTags.Select(o => new OptionDto(o.EngageTagId, o.EngageTag.Name))))
            .ForMember(d => d.UnitTypeId, opt => opt.Ignore());
    }
}
