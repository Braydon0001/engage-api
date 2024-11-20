namespace Engage.Application.Services.EngageDepartmentGroups.Commands;

public class EngageDepartmentGroupCommand : IMapTo<EngageDepartmentGroup>
{
    public string Name { get; set; }
    public string Description { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EngageDepartmentGroupCommand, EngageDepartmentGroup>();
    }
}
