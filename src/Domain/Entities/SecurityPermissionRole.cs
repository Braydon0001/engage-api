namespace Engage.Domain.Entities;

public class SecurityPermissionRole : BaseAuditableEntity
{
    public int SecurityPermissionRoleId { get; set; }
    public int SecurityRoleId { get; set; }
    public int SecurityPermissionId { get; set; }

    // Navigation Properties

    public SecurityRole SecurityRole { get; set; }
    public SecurityPermission SecurityPermission { get; set; }
}