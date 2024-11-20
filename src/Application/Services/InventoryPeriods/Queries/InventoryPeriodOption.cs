// auto-generated
namespace Engage.Application.Services.InventoryPeriods.Queries;

public class InventoryPeriodOption : IMapFrom<InventoryPeriod>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<InventoryPeriod, InventoryPeriodOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.InventoryPeriodId));
    }
}