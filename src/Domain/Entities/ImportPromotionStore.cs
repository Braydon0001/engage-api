using Engage.Domain.Common;
using Engage.Domain.Enums;

namespace Engage.Domain.Entities
{
    public class ImportPromotionStore : BaseAuditableEntity
    {
        public int ImportPromotionStoreId { get; set; }
        public int ImportFileId { get; set; }
        public ImportRowType ImportRowType { get; set; }
        public string ImportRowMessage { get; set; }
        public int RowNo { get; set; }
        public int PromotionId { get; set; }
        public int? StoreId { get; set; }
        public string AccountNumber { get; set; }

        // Navigation Properties
        public ImportFile FileImport { get; set; }
        public Promotion Promotion { get; set; }
        public Store Store { get; set; }
    }
}
