namespace Engage.Domain.Entities;

public class UserOrganizationRole : BaseAuditableEntity
{
    public int UserOrganizationRoleId { get; set; }
    public int UserId { get; set; }
    public int UserOrganizationId { get; set; }
    public int UserRoleId { get; set; }

    // Navigation Properties

    public User User { get; set; }
    public UserOrganization UserOrganization { get; set; }
    public UserRole UserRole { get; set; }
}