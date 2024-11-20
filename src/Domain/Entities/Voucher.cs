namespace Engage.Domain.Entities;

public class Voucher : BaseAuditableEntity
{
    public Voucher()
    {
        VoucherDetails = new HashSet<VoucherDetail>();
    }
    public int VoucherId { get; set; }
    public string Name { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal Total { get; set; }
    public string Note { get; set; }

    // Foreign Keys
    public int VoucherStatusId { get; set; }
    public int SupplierId { get; set; }
    public int EngageRegionId { get; set; }

    // Audit Fields
    public DateTime? ClosedDate { get; set; }
    public string ClosedBy { get; set; }

    //Navigation Props
    public VoucherStatus VoucherStatus { get; set; }
    public Supplier Supplier { get; set; }
    public EngageRegion EngageRegion { get; set; }
    public ICollection<VoucherDetail> VoucherDetails { get; set; }
}
