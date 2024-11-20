namespace Engage.Domain.Entities;

public class SecurityPermission : BaseAuditableEntity
{
    public SecurityPermission()
    {
        SecurityPermissionRoles = new HashSet<SecurityPermissionRole>();
    }
    public int SecurityPermissionId { get; set; }
    public string Name { get; set; }
    public string Key { get; set; }
    public string Description { get; set; }

    //Navigation Properties
    public ICollection<SecurityPermissionRole> SecurityPermissionRoles { get; set; }
}