
namespace Engage.Application.Services.SparAnalysisGroups.Queries;

public class SparAnalysisGroupVm : IMapFrom<SparAnalysisGroup>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SparAnalysisGroup, SparAnalysisGroupVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SparAnalysisGroupId));
    }
}
