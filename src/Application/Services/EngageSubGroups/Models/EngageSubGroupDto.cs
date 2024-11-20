namespace Engage.Application.Services.EngageSubGroups.Models;

public class EngageSubGroupDto : IMapFrom<EngageSubGroup>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int? Order { get; set; }
    public int EngageGroupId { get; set; }
    public string EngageGroupName { get; set; }
    public int StoreDepartmentId { get; set; }
    public string StoreDepartmentName { get; set; }
    public int EngageDepartmentId { get; set; }
    public string EngageDepartmentName { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EngageSubGroup, EngageSubGroupDto>();
    }
}
