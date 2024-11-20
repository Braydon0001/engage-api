namespace Engage.Domain.Entities;

public class UserPermission : BaseAuditableEntity
{
    public int UserPermissionId { get; set; }
    public string Name { get; set; }
    public string Key { get; set; }
    public string Description { get; set; }
}