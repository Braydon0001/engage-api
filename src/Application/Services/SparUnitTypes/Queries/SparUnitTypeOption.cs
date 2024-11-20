namespace Engage.Application.Services.SparUnitTypes.Queries;

public class SparUnitTypeOption : IMapFrom<SparUnitType>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SparUnitType, SparUnitTypeOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SparUnitTypeId));
    }
}