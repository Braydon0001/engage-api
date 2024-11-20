namespace Engage.Application.Services.EngageDepartments.Commands;

public class EngageDepartmentCommand : IMapTo<EngageDepartment>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int EngageDepartmentGroupId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EngageDepartmentCommand, EngageDepartment>();
    }
}
