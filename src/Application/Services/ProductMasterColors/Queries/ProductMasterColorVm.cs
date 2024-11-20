// auto-generated
using Engage.Application.Services.ProductMasters.Queries;

namespace Engage.Application.Services.ProductMasterColors.Queries;

public class ProductMasterColorVm : IMapFrom<ProductMasterColor>
{
    public int Id { get; set; }
    public ProductMasterOption ProductMasterId { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductMasterColor, ProductMasterColorVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductMasterColorId))
               .ForMember(d => d.ProductMasterId, opt => opt.MapFrom(s => s.ProductMaster));
    }
}
