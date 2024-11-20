namespace Engage.Domain.Entities;

public class ProjectCategorySupplier : BaseAuditableEntity
{
    public int ProjectCategorySupplierId { get; set; }
    public int ProjectCategoryId { get; set; }
    public int SupplierId { get; set; }

    // Navigation Properties

    public ProjectCategory ProjectCategory { get; set; }
    public Supplier Supplier { get; set; }
}