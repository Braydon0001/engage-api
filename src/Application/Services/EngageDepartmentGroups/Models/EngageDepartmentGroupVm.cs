namespace Engage.Application.Services.EngageDepartmentGroups.Models;

public class EngageDepartmentGroupVm : IMapFrom<EngageDepartmentGroup>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int? Order { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EngageDepartmentGroup, EngageDepartmentGroupVm>();
    }
}
