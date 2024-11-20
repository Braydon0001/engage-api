namespace Engage.Domain.Entities;

public class Organization : BaseAuditableEntity
{
    public int OrganizationId { get; set; }
    public int? OrganizationSettingId { get; set; }
    public string Name { get; set; }
    public string TenantIdentifier { get; set; }
    public List<JsonSetting> Settings { get; set; }

    public string ThemeColor { get; set; }
    public string ThemeCustomColor { get; set; }
    public JsonThemeSetting JsonTheme { get; set; }
    public List<JsonFile> Files { get; set; }

    // Navigation Properties
    public OrganizationSetting OrganizationSetting { get; set; }
}