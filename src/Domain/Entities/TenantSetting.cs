namespace Engage.Domain.Entities;

public class TenantSetting : BaseAuditableEntity
{
    public int TenantSettingId { get; set; }
    public int SettingId { get; set; }
    public string Value { get; set; }

    // Navigation Properties
    public Setting Setting { get; set; }
}
