namespace Engage.Domain.Entities;

public class SecurityRoleUser : BaseAuditableEntity
{
    public int SecurityRoleUserId { get; set; }
    public int UserId { get; set; }
    public int SecurityRoleId { get; set; }

    // Navigation Properties

    public User User { get; set; }
    public SecurityRole SecurityRole { get; set; }
}