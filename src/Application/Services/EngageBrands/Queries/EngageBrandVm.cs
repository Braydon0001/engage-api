namespace Engage.Application.Services.EngageCategories.Models;

public class EngageBrandVm : IMapFrom<EngageBrand>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsSparBrand { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EngageBrand, EngageBrandVm>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id));
    }
}
