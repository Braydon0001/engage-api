namespace Engage.Domain.Entities;

public class ClaimClassification : BaseAuditableEntity
{
    public ClaimClassification()
    {
        SupplierClaimClassifications = new HashSet<SupplierClaimClassification>();
        ClaimClassificationTypes = new HashSet<ClaimClassificationType>();
    }

    public int ClaimClassificationId { get; set; }
    public string Name { get; set; }
    public bool IsPayStore { get; set; }
    public bool EditIsPayStore { get; set; }
    public bool IsClaimFromSupplier { get; set; }
    public bool EditIsClaimFromSupplier { get; set; }
    public bool IsDairy { get; set; }
    public bool IsSupplierProcess { get; set; }
    public int? ClaimTypeId { get; set; }
    public int? SupplierId { get; set; }
    public bool IsAttachmentRequiredOnSubmit { get; set; }

    //Navigation Properties
    public ClaimType ClaimType { get; set; }
    public Supplier Supplier { get; set; }
    public ICollection<SupplierClaimClassification> SupplierClaimClassifications { get; set; }
    public ICollection<ClaimClassificationType> ClaimClassificationTypes { get; set; }
}

