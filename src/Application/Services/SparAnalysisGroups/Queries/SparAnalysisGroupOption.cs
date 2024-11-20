namespace Engage.Application.Services.SparAnalysisGroups.Queries;

public class SparAnalysisGroupOption : IMapFrom<SparAnalysisGroup>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SparAnalysisGroup, SparAnalysisGroupOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SparAnalysisGroupId));
    }
}