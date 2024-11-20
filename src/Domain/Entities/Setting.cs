namespace Engage.Domain.Entities
{
    public class Setting : BaseAuditableEntity
    {
        public int SettingId { get; set; }
        public string Name { get; set; }
    }
}
