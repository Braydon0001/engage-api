// auto-generated
using Engage.Application.Services.InventoryYears.Queries;

namespace Engage.Application.Services.InventoryPeriods.Queries;

public class InventoryPeriodVm : IMapFrom<InventoryPeriod>
{
    public int Id { get; set; }
    public InventoryYearOption InventoryYearId { get; set; }
    public string Name { get; set; }
    public int Number { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<InventoryPeriod, InventoryPeriodVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.InventoryPeriodId))
               .ForMember(d => d.InventoryYearId, opt => opt.MapFrom(s => s.InventoryYear));
    }
}
