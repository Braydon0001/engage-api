// auto-generated
namespace Engage.Application.Services.ProductGroups.Queries;

public class ProductGroupVm : IMapFrom<ProductGroup>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductGroup, ProductGroupVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductGroupId));
    }
}
