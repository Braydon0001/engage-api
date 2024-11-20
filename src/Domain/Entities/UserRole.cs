namespace Engage.Domain.Entities;

public class UserRole : BaseAuditableEntity
{
    public int UserRoleId { get; set; }
    public string Name { get; set; }
    public string Key { get; set; }
    public string Description { get; set; }
}