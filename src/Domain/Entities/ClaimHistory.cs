namespace Engage.Domain.Entities;

public class ClaimHistory : BaseAuditableEntity
{
    public int ClaimHistoryId { get; set; }
    public int? ClaimHistoryHeaderId { get; set; }
    public int ClaimId { get; set; }
    public int ClaimStatusId { get; set; }
    public int? ClaimSupplierStatusId { get; set; }
    public int? ClaimPendingReasonId { get; set; }
    public string PendingReason { get; set; }
    public int? ClaimRejectedReasonId { get; set; }
    public string RejectedReason { get; set; }

    //Navigation Props
    public ClaimHistoryHeader ClaimHistoryHeader { get; set; }
    public Claim Claim { get; set; }
    public ClaimStatus ClaimStatus { get; set; }
    public ClaimSupplierStatus ClaimSupplierStatus { get; set; }
    public ClaimRejectedReason ClaimRejectedReason { get; set; }
    public ClaimPendingReason ClaimPendingReason { get; set; }

}
