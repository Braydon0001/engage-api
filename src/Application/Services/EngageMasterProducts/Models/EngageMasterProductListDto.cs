using Engage.Application.Services.EngageVariantProducts.Models;

namespace Engage.Application.Services.EngageMasterProducts.Models;

public class EngageMasterProductListDto : IMapFrom<EngageMasterProduct>
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string EngageSubCategoryName { get; set; }
    public bool Disabled { get; set; }
    public bool Deleted { get; set; }
    public List<JsonFile> Files { get; set; }
    public ICollection<EngageVariantProductListDto> EngageVariantProducts { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EngageMasterProduct, EngageMasterProductListDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EngageMasterProductId));
    }
}
