namespace Engage.Application.Services.SparUnitTypes.Queries;

public class SparUnitTypeDto : IMapFrom<SparUnitType>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SparUnitType, SparUnitTypeDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SparUnitTypeId));
    }
}
