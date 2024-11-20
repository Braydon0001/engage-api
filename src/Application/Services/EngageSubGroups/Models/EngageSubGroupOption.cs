namespace Engage.Application.Services.EngageSubGroups.Models;

public class EngageSubGroupOption : IMapFrom<EngageSubGroup>
{
    public int Id { get; set; }
    public int ParentId { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EngageSubGroup, EngageSubGroupOption>()
               .ForMember(d => d.ParentId, opt => opt.MapFrom(s => s.EngageGroupId));
    }
}
