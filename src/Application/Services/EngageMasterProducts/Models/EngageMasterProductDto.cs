namespace Engage.Application.Services.EngageMasterProducts.Models;

public class EngageMasterProductDto : IMapFrom<EngageMasterProduct>
{
    public int Id { get; set; }
    public int SupplierId { get; set; }
    public int ProductClassificationId { get; set; }
    public int EngageDepartmentId { get; set; }
    public int EngageBrandId { get; set; }
    public int VatId { get; set; }
    public int EngageGroupId { get; set; }
    public int EngageSubGroupId { get; set; }
    public int EngageCategoryId { get; set; }
    public int EngageSubCategoryId { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public bool IsVATProduct { get; set; }
    public bool IsDairyProduct { get; set; }
    public bool Disabled { get; set; }
    public bool IsAllSuppliersProduct { get; set; }
    public bool IsFreshProduct { get; set; }
    public bool IsDropShipment { get; set; }

    public List<JsonFile> Files { get; set; }
    public ICollection<OptionDto> EngageTags { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EngageMasterProduct, EngageMasterProductDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EngageMasterProductId))
            .ForMember(d => d.EngageGroupId, opt => opt.MapFrom(s => s.EngageSubCategory.EngageCategory.EngageSubGroup.EngageGroupId))
            .ForMember(d => d.EngageSubGroupId, opt => opt.MapFrom(s => s.EngageSubCategory.EngageCategory.EngageSubGroupId))
            .ForMember(d => d.EngageCategoryId, opt => opt.MapFrom(s => s.EngageSubCategory.EngageCategoryId))
            .ForMember(d => d.EngageTags, opt => opt.MapFrom(s => s.EngageProductTags
                                                                       .Select(s => s.EngageTag)
                                                                       .Select(s => new OptionDto() { Id = s.Id, Name = s.Name })));
    }
}
