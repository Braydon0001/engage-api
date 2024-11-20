namespace Engage.Application.Services.EngageGroups.Commands;

public class EngageGroupCommand : IMapTo<EngageGroup>
{
    public string Name { get; set; }
    public string Description { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EngageGroupCommand, EngageGroup>();
    }
}
