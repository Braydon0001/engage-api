namespace Engage.Application.Services.EmployeeJobTitleUserGroups.Commands;

public class EmployeeJobTitleUserGroupCommand : IMapTo<EmployeeJobTitleUserGroup>
{
    public int EmployeeJobTitleId { get; set; }
    public int UserGroupId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeJobTitleUserGroupCommand, EmployeeJobTitleUserGroup>();
    }
}
