namespace Engage.Application.Services.EngageGroups.Models;

public class EngageGroupDto : IMapFrom<EngageGroup>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int? Order { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EngageGroup, EngageGroupDto>();
    }
}
