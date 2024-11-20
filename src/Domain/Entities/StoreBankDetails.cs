using Engage.Domain.Common;

namespace Engage.Domain.Entities
{
    public class StoreBankDetail : BaseAuditableEntity
    {
        public int StoreBankDetailId { get; set; }
        public int StoreId { get; set; }
        public string Bank { get; set; }
        public string BranchCode { get; set; }
        public string AccountNumber { get; set; }
        public string AccountHolder { get; set; }
        public bool IsPrimary { get; set; }
        public string Note { get; set; }

        // Navigation Properties
        public Store Store { get; set; }
    }
}
