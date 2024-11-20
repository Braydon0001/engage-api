namespace Engage.Domain.Entities;

public class RoleUserGroup : BaseAuditableEntity
{
    public int RoleUserGroupId { get; set; }
    public int RoleId { get; set; }
    public int UserGroupId { get; set; }

    // Navigation Properties

    public Role Role { get; set; }
    public UserGroup UserGroup { get; set; }
}