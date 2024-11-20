namespace Engage.Domain.Entities;

public class ProjectProjectTagSupplier : ProjectProjectTag
{
    public int SupplierId { get; set; }
    public Supplier Supplier { get; set; }
}
