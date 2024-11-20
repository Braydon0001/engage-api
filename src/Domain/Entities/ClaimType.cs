namespace Engage.Domain.Entities;

public class ClaimType : BaseAuditableEntity
{
    public ClaimType()
    {
        ClaimTypeReportTypes = new HashSet<ClaimTypeReportType>();
        ClaimClassificationTypes = new HashSet<ClaimClassificationType>();
    }

    public int ClaimTypeId { get; set; }
    public string Name { get; set; }
    public int VatId { get; set; }
    public bool IsVatInclusive { get; set; }
    public bool IsDairy { get; set; }
    public bool IsEmployeeClaim { get; set; }
    public int? SupplierId { get; set; }

    // Navigation Properties
    public Vat Vat { get; set; }
    public Supplier Supplier { get; set; }

    public ICollection<ClaimTypeReportType> ClaimTypeReportTypes { get; set; }
    public ICollection<ClaimClassificationType> ClaimClassificationTypes { get; set; }
}
