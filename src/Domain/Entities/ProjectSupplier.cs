namespace Engage.Domain.Entities;

public class ProjectSupplier : BaseAuditableEntity
{
    public int ProjectSupplierId { get; set; }
    public int ProjectId { get; set; }
    public int SupplierId { get; set; }

    // Navigation Properties

    public Project Project { get; set; }
    public Supplier Supplier { get; set; }
}