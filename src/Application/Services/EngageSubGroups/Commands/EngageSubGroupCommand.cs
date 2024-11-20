namespace Engage.Application.Services.EngageSubGroups.Commands;

public class EngageSubGroupCommand : IMapTo<EngageSubGroup>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int EngageGroupId { get; set; }
    public int StoreDepartmentId { get; set; }
    public int EngageDepartmentId { get; set; }
    public List<int> SupplierIds { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EngageSubGroupCommand, EngageSubGroup>();
    }
}
