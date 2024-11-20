namespace Engage.Application.Services.EngageDepartments.Models;

public class EngageDepartmentDto : IMapFrom<EngageDepartment>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int? Order { get; set; }
    public int EngageDepartmentGroupId { get; set; }
    public string EngageDepartmentGroupName { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EngageDepartment, EngageDepartmentDto>();
    }
}
