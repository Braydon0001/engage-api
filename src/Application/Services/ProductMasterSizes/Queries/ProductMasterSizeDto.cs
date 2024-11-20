// auto-generated
namespace Engage.Application.Services.ProductMasterSizes.Queries;

public class ProductMasterSizeDto : IMapFrom<ProductMasterSize>
{
    public int Id { get; set; }
    public int ProductMasterId { get; set; }
    public string ProductMasterName { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductMasterSize, ProductMasterSizeDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductMasterSizeId))
               .ForMember(d => d.ProductMasterName, opt => opt.MapFrom(s => s.ProductMaster.Code + " - " + s.ProductMaster.Description));
    }
}
