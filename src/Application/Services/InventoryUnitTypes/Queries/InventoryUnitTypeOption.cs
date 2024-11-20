// auto-generated
namespace Engage.Application.Services.InventoryUnitTypes.Queries;

public class InventoryUnitTypeOption : IMapFrom<InventoryUnitType>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<InventoryUnitType, InventoryUnitTypeOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.InventoryUnitTypeId));
    }
}