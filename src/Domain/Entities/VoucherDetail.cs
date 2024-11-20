namespace Engage.Domain.Entities;

public class VoucherDetail : BaseAuditableEntity
{
    public int VoucherDetailId { get; set; }
    public int VoucherId { get; set; }
    public int VoucherDetailStatusId { get; set; }
    public int? EmployeeId { get; set; }
    public int? StoreId { get; set; }
    public int? StoreContactId { get; set; }
    public int? ClaimId { get; set; }
    public string VoucherNumber { get; set; }
    public decimal Amount { get; set; }
    public string Note { get; set; }
    public List<JsonFile> Files { get; set; }

    // Audit Fields
    public DateTime? AssignedDate { get; set; }
    public string AssignedBy { get; set; }
    public DateTime? ClosedDate { get; set; }
    public string ClosedBy { get; set; }

    //Navigation Props
    public Voucher Voucher { get; set; }
    public VoucherDetailStatus VoucherDetailStatus { get; set; }
    public Employee Employee { get; set; }
    public Store Store { get; set; }
    public StoreContact StoreContact { get; set; }
    public Claim Claim { get; set; }
}
