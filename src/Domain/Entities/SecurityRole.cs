namespace Engage.Domain.Entities;

public class SecurityRole : BaseAuditableEntity
{
    public SecurityRole()
    {
        SecurityPermissionRoles = new HashSet<SecurityPermissionRole>();
        SecurityRoleUsers = new HashSet<SecurityRoleUser>();
    }
    public int SecurityRoleId { get; set; }
    public string Name { get; set; }
    public string Key { get; set; }
    public string Description { get; set; }

    // Navigation Properties
    public ICollection<SecurityPermissionRole> SecurityPermissionRoles { get; set; }
    public ICollection<SecurityRoleUser> SecurityRoleUsers { get; set; }
}