// auto-generated
namespace Engage.Application.Services.InventoryUnitTypes.Queries;

public class InventoryUnitTypeDto : IMapFrom<InventoryUnitType>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<InventoryUnitType, InventoryUnitTypeDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.InventoryUnitTypeId));
    }
}
