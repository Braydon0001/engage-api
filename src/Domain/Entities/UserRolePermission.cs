namespace Engage.Domain.Entities;

public class UserRolePermission : BaseAuditableEntity
{
    public int UserRolePermissionId { get; set; }
    public int UserRoleId { get; set; }
    public int UserPermissionId { get; set; }

    // Navigation Properties

    public UserRole UserRole { get; set; }
    public UserPermission UserPermission { get; set; }
}