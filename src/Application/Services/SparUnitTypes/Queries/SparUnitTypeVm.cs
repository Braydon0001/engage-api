
namespace Engage.Application.Services.SparUnitTypes.Queries;

public class SparUnitTypeVm : IMapFrom<SparUnitType>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SparUnitType, SparUnitTypeVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SparUnitTypeId));
    }
}
