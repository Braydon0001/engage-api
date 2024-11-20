namespace Engage.Domain.Entities;

public class EmployeeJobTitleUserGroup
{
    public int EmployeeJobTitleUserGroupId { get; set; }
    public int EmployeeJobTitleId { get; set; }
    public int UserGroupId { get; set; }
    public EmployeeJobTitle EmployeeJobTitle { get; set; }
    public UserGroup UserGroup { get; set; }
}
