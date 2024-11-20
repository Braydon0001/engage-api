// auto-generated

namespace Engage.Application.Services.EngageDepartments.Models;

public class EngageDepartmentOption : IMapFrom<EngageDepartment>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EngageDepartment, EngageDepartmentOption>();
    }
}