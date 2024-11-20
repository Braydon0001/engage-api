namespace Engage.Application.Services.CostTypes.Queries;

public class CostTypeOption : IMapFrom<CostType>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CostType, CostTypeOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.CostTypeId));
    }
}