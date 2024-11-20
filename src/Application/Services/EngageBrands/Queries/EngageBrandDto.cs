namespace Engage.Application.Services.EngageBrands.Queries;

public class EngageBrandDto : IMapFrom<EngageBrand>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsSparBrand { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EngageBrand, EngageBrandDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id));
    }
}
