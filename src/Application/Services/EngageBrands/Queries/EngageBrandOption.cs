namespace Engage.Application.Services.EngageBrands.Queries;

public class EngageBrandOption : IMapFrom<EngageBrand>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EngageBrand, EngageBrandOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id));
    }
}
