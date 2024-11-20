// auto-generated
namespace Engage.Application.Services.ProductGroups.Queries;

public class ProductGroupOption : IMapFrom<ProductGroup>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductGroup, ProductGroupOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductGroupId));
    }
}