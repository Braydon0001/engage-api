using Engage.Application.Services.DCProducts.Models;

namespace Engage.Application.Services.EngageVariantProducts.Models;

public class EngageVariantProductListDto : IMapFrom<EngageVariantProduct>
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public int EngageMasterProductId { get; set; }
    public string EngageMasterProductCode { get; set; }
    public string EngageMasterProductName { get; set; }
    public string EngageMasterProductSubCategoryName { get; set; }
    public bool EngageMasterProductDisabled { get; set; }
    public bool EngageMasterProductDeleted { get; set; }
    public bool Disabled { get; set; }
    public bool Deleted { get; set; }
    public List<JsonFile> Files { get; set; }
    public ICollection<DCProductListDto> DCProducts { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EngageVariantProduct, EngageVariantProductListDto>()
            .ForMember(e => e.Id, opt => opt.MapFrom(d => d.EngageVariantProductId))
            .ForMember(e => e.EngageMasterProductCode, opt => opt.MapFrom(d => d.EngageMasterProduct.Code))
            .ForMember(e => e.EngageMasterProductName, opt => opt.MapFrom(d => d.EngageMasterProduct.Name))
            .ForMember(e => e.EngageMasterProductSubCategoryName, opt => opt.MapFrom(d => d.EngageMasterProduct.EngageSubCategory.Name))
            .ForMember(e => e.EngageMasterProductDisabled, opt => opt.MapFrom(d => d.EngageMasterProduct.Disabled))
            .ForMember(e => e.EngageMasterProductDeleted, opt => opt.MapFrom(d => d.EngageMasterProduct.Deleted));
    }
}
