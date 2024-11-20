
namespace Engage.Application.Services.CostTypes.Queries;

public class CostTypeVm : IMapFrom<CostType>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CostType, CostTypeVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.CostTypeId));
    }
}
