namespace Engage.Domain.Entities;

public class CategoryTarget : BaseAuditableEntity
{
    public int CategoryTargetId { get; set; }
    public int SupplierId { get; set; }
    public float? Target { get; set; }
    public string AvailableLabel { get; set; }
    public string OccupiedLabel { get; set; }
    public string TextQuestion { get; set; }
    public int? CategoryTargetTypeId { get; set; }



    // Navigation Properties


    public Supplier Supplier { get; set; }
    public CategoryTargetType CategoryTargetType { get; set; }
}