namespace Engage.Application.Services.InventoryYears.Queries;

public class InventoryYearOption : IMapFrom<InventoryYear>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<InventoryYear, InventoryYearOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.InventoryYearId));
    }
}