namespace Engage.Domain.Entities;

public class UserGroup : BaseAuditableEntity
{
    public UserGroup()
    {
        Users = new HashSet<UserUserGroup>();
        EmployeeJobTitleUserGroups = new HashSet<EmployeeJobTitleUserGroup>();
        RoleUserGroups = new HashSet<RoleUserGroup>();
    }
    public int UserGroupId { get; set; }
    public int? EngageRegionId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string VendorId { get; set; }
    // Navigation Properties
    public EngageRegion EngageRegion { get; set; }
    public ICollection<UserUserGroup> Users { get; set; }
    public ICollection<EmployeeJobTitleUserGroup> EmployeeJobTitleUserGroups { get; set; }
    public ICollection<RoleUserGroup> RoleUserGroups { get; set; }
}
