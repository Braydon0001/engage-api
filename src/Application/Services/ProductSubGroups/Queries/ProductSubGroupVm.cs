// auto-generated
using Engage.Application.Services.ProductGroups.Queries;

namespace Engage.Application.Services.ProductSubGroups.Queries;

public class ProductSubGroupVm : IMapFrom<ProductSubGroup>
{
    public int Id { get; set; }
    public ProductGroupOption ProductGroupId { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductSubGroup, ProductSubGroupVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductSubGroupId))
               .ForMember(d => d.ProductGroupId, opt => opt.MapFrom(s => s.ProductGroup));
    }
}
