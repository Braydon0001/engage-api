namespace Engage.Application.Services.InventoryYears.Queries;

public class InventoryYearDto : IMapFrom<InventoryYear>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<InventoryYear, InventoryYearDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.InventoryYearId));
    }
}
