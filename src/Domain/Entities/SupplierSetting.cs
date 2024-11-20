using Engage.Domain.Common;

namespace Engage.Domain.Entities
{
    public class SupplierSetting : BaseAuditableEntity
    {
        public int SupplierSettingId { get; set; }
        public int SupplierId { get; set; }
        public int SettingId { get; set; }
        public string Value { get; set; }

        // Navigation Properties
        public Supplier Supplier { get; set; }
        public Setting Setting { get; set; }
    }
}
