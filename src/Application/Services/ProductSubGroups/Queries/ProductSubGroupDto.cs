// auto-generated
namespace Engage.Application.Services.ProductSubGroups.Queries;

public class ProductSubGroupDto : IMapFrom<ProductSubGroup>
{
    public int Id { get; set; }
    public int ProductGroupId { get; set; }
    public string ProductGroupName { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductSubGroup, ProductSubGroupDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductSubGroupId));
    }
}
