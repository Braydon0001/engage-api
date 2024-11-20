namespace Engage.Domain.Entities;

public class PaymentProof : BaseAuditableEntity
{
    public int PaymentProofId { get; set; }
    public List<JsonFile> Files { get; set; }
}