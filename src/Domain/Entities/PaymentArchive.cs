namespace Engage.Domain.Entities;

public class PaymentArchive : BaseAuditableEntity
{
    public PaymentArchive()
    {
        Payments = new HashSet<Payment>();
    }
    public int PaymentArchiveId { get; set; }
    public DateTime ArchiveDate { get; set; }
    public List<JsonFile> Files { get; set; }

    public ICollection<Payment> Payments { get; set; }
}
