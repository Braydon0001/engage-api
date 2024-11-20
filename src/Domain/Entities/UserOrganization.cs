namespace Engage.Domain.Entities;

public class UserOrganization : BaseAuditableEntity
{
    public int UserOrganizationId { get; set; }
    public string Name { get; set; }
    public int SupplierId { get; set; }
    public string ThemeName { get; set; }
    public string ThemeColor { get; set; }
    public List<JsonFile> Files { get; set; }

    // Navigation Properties

    public Supplier Supplier { get; set; }
}