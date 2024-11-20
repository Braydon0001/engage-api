namespace Engage.Domain.Entities;

public class Claim : BaseAuditableEntity
{
    public Claim()
    {
        ClaimSkus = new HashSet<ClaimSku>();
        ClaimHistory = new HashSet<ClaimHistory>();
        ClaimBlobs = new HashSet<ClaimBlob>();
        ClaimFloatClaims = new HashSet<ClaimFloatClaim>();
    }
    public int ClaimId { get; set; }
    public string ClaimNumber { get; set; }
    public bool IsPayStore { get; set; }
    public bool IsClaimFromSupplier { get; set; }
    public bool IsVatInclusive { get; set; }
    public bool IsDairy { get; set; }
    public DateTime ClaimDate { get; set; }
    public string ClaimReference { get; set; }
    public string Comment { get; set; }

    // Status Changes
    public DateTime? UnapprovedDate { get; set; }
    public string UnapprovedBy { get; set; }
    public DateTime? ApprovedDate { get; set; }
    public string ApprovedBy { get; set; }
    public DateTime? SupplierApprovedDate { get; set; }
    public string SupplierApprovedBy { get; set; }
    public DateTime? PaidDate { get; set; }
    public string PaidBy { get; set; }
    public DateTime? RejectedDate { get; set; }
    public string RejectedBy { get; set; }
    public int? ClaimRejectedReasonId { get; set; }
    public string RejectedReason { get; set; }
    public DateTime? PendingDate { get; set; }
    public string PendingBy { get; set; }
    public int? ClaimPendingReasonId { get; set; }
    public string PendingReason { get; set; }

    // Foreign Keys
    public int ClientTypeId { get; set; }
    public int ClaimTypeId { get; set; }
    public int ClaimStatusId { get; set; }
    public int ClaimSupplierStatusId { get; set; }
    public int ClaimClassificationId { get; set; }
    public int VatId { get; set; }
    public int SupplierId { get; set; }
    public int StoreId { get; set; }
    public int DistributionCenterId { get; set; }
    public int ClaimPeriodId { get; set; }
    public int? ClaimAccountManagerId { get; set; }
    public int? ClaimManagerId { get; set; }
    public int? ClaimFloatId { get; set; }
    public int? EmployeeDivisionId { get; set; }

    //Navigation Props
    public ClientType ClientType { get; set; }
    public ClaimType ClaimType { get; set; }
    public ClaimStatus ClaimStatus { get; set; }
    public ClaimSupplierStatus ClaimSupplierStatus { get; set; }
    public ClaimClassification ClaimClassification { get; set; }
    public Vat Vat { get; set; }
    public Supplier Supplier { get; set; }
    public Store Store { get; set; }
    public DistributionCenter DistributionCenter { get; set; }
    public ClaimPeriod ClaimPeriod { get; set; }
    public User ClaimAccountManager { get; set; }
    public User ClaimManager { get; set; }
    public ClaimFloat ClaimFloat { get; set; }
    public EmployeeDivision EmployeeDivision { get; set; }
    public ICollection<ClaimSku> ClaimSkus { get; set; }
    public ICollection<ClaimHistory> ClaimHistory { get; set; }
    public ICollection<ClaimBlob> ClaimBlobs { get; set; }
    public ICollection<ClaimFloatClaim> ClaimFloatClaims { get; set; }

}
