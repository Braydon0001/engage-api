namespace Engage.Domain.Entities;
public class User : BaseAuditableEntity
{
    public User()
    {
        EngageRegionClaimManagers = new HashSet<EngageRegionClaimManager>();
        SupplierClaimAccountManagers = new HashSet<SupplierClaimAccountManager>();
        UserGroups = new HashSet<UserUserGroup>();
        //SecurityRoles = new HashSet<SecurityRoleUser>();
        ProjectUsers = new HashSet<ProjectUser>();
        UserEngageSubGroups = new HashSet<UserEngageSubGroup>();
        UserCommunicationTypes = new HashSet<UserCommunicationType>();
        UserRegions = new HashSet<UserRegion>();
    }
    public int UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string MobilePhone { get; set; }
    public string ExternalId { get; set; }
    public int SupplierId { get; set; }
    public List<JsonSetting> Settings { get; set; }
    public bool IgnoreOrderProductFilters { get; set; }
    //public int? SecurityOrganizationId { get; set; }
    public int? OrganizationId { get; set; }
    public int? RoleId { get; set; }

    // Navigation Properties
    public Supplier Supplier { get; set; }
    //public SecurityOrganization SecurityOrganization { get; set; }
    public Organization Organization { get; set; }
    public Role Role { get; set; }
    public ICollection<EngageRegionClaimManager> EngageRegionClaimManagers { get; set; }
    public ICollection<SupplierClaimAccountManager> SupplierClaimAccountManagers { get; set; }
    public ICollection<UserUserGroup> UserGroups { get; set; }
    //public ICollection<SecurityRoleUser> SecurityRoles { get; set; }
    public ICollection<ProjectUser> ProjectUsers { get; set; }
    public ICollection<UserEngageSubGroup> UserEngageSubGroups { get; set; }
    public ICollection<UserCommunicationType> UserCommunicationTypes { get; set; }
    public ICollection<UserRegion> UserRegions { get; set; }
}
