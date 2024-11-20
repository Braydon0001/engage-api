namespace Engage.Application.Services.EmployeeJobTitleUserGroups.Models;

public class EmployeeJobTitleUserGroupDto : IMapFrom<EmployeeJobTitleUserGroup>
{
    public int Id { get; set; }
    public int EmployeeJobTitleIdId { get; set; }
    public string EmployeeJobTitleName { get; set; }
    public int UserGroupId { get; set; }
    public string UserGroupName { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeJobTitleUserGroup, EmployeeJobTitleUserGroupDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeJobTitleUserGroupId));
    }

}
