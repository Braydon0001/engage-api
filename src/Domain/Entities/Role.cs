namespace Engage.Domain.Entities;

public class Role : BaseAuditableEntity
{
    public Role()
    {
        RoleUserGroups = new HashSet<RoleUserGroup>();
        Users = new HashSet<User>();
    }
    public int RoleId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    // Navigation Properties
    public ICollection<RoleUserGroup> RoleUserGroups { get; set; }
    public ICollection<User> Users { get; set; }
}