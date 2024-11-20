namespace Engage.Application.Services.CostCenters.Queries;

public class CostCenterOption : IMapFrom<CostCenter>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CostCenter, CostCenterOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.CostCenterId));
    }
}