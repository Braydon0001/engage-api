// auto-generated
namespace Engage.Application.Services.ProductMasterColors.Queries;

public class ProductMasterColorDto : IMapFrom<ProductMasterColor>
{
    public int Id { get; set; }
    public int ProductMasterId { get; set; }
    public string ProductMasterName { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductMasterColor, ProductMasterColorDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductMasterColorId))
               .ForMember(d => d.ProductMasterName, opt => opt.MapFrom(s => s.ProductMaster.Code + " - " + s.ProductMaster.Description));
    }
}
