// auto-generated
namespace Engage.Application.Services.ProductSubGroups.Queries;

public class ProductSubGroupOption : IMapFrom<ProductSubGroup>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductSubGroup, ProductSubGroupOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductSubGroupId));
    }
}