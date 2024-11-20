

namespace Engage.Domain.Entities;

public class OrganizationSetting: BaseAuditableEntity
{
    public int OrganizationSettingId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string FaviconUrl { get; set; }
    public string LogoUrl { get; set; }
    public string LogoDarkUrl { get; set; }
    public JsonThemeSetting OrganizationTheme { get; set; }
}
