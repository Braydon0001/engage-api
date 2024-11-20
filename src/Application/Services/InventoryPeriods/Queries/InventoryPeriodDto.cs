// auto-generated
namespace Engage.Application.Services.InventoryPeriods.Queries;

public class InventoryPeriodDto : IMapFrom<InventoryPeriod>
{
    public int Id { get; set; }
    public int InventoryYearId { get; set; }
    public string InventoryYearName { get; set; }
    public string Name { get; set; }
    public int Number { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<InventoryPeriod, InventoryPeriodDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.InventoryPeriodId));
    }
}
