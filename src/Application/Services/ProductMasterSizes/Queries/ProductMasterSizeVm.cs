// auto-generated
using Engage.Application.Services.ProductMasters.Queries;

namespace Engage.Application.Services.ProductMasterSizes.Queries;

public class ProductMasterSizeVm : IMapFrom<ProductMasterSize>
{
    public int Id { get; set; }
    public ProductMasterOption ProductMasterId { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductMasterSize, ProductMasterSizeVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductMasterSizeId))
               .ForMember(d => d.ProductMasterId, opt => opt.MapFrom(s => s.ProductMaster));
    }
}
