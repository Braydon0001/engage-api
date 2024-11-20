namespace Engage.Domain.Entities;

public class Payment : BaseAuditableEntity
{
    public Payment()
    {
        PaymentLines = new HashSet<PaymentLine>();
        PaymentStatusHistories = new HashSet<PaymentStatusHistory>();
    }
    public int PaymentId { get; set; }
    public int PaymentBatchId { get; set; }
    public int? PaymentArchiveId { get; set; }
    public int CreditorId { get; set; }
    public int PaymentStatusId { get; set; }
    public int? VatId { get; set; }
    public int PaymentPeriodId { get; set; }
    public string InvoiceNumber { get; set; }
    public DateTime InvoiceDate { get; set; }
    public bool IsClaimFromSupplier { get; set; }
    public List<JsonFile> Files { get; set; }

    // Navigation Properties

    public Creditor Creditor { get; set; }
    public PaymentBatch PaymentBatch { get; set; }
    public PaymentArchive PaymentArchive { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
    public Vat Vat { get; set; }
    public PaymentPeriod PaymentPeriod { get; set; }
    public ICollection<PaymentLine> PaymentLines { get; set; }
    public ICollection<PaymentStatusHistory> PaymentStatusHistories { get; set; }
}