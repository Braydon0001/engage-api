using Engage.Domain.Common;

namespace Engage.Domain.Entities
{
    public class ClaimBatchDetail: BaseAuditableEntity
    {
        public int ClaimBatchDetailId { get; set; }
        public int ClaimBatchId { get; set; }
        public int ClaimId { get; set; }
        public string Message { get; set; }
        
        // Navigation Properties
        public ClaimBatch ClaimBatch { get; set; }
        public Claim Claim { get; set; }
        
    }
}
