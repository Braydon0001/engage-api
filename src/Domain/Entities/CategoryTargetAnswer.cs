namespace Engage.Domain.Entities;

public class CategoryTargetAnswer : BaseAuditableEntity
{
    public int CategoryTargetAnswerId { get; set; }
    public int CategoryTargetId { get; set; }
    public int CategoryTargetStoreId { get; set; }
    public int? EmployeeId { get; set; }
    public float? Target { get; set; }
    public float? Available { get; set; }
    public float? Occupied { get; set; }
    public DateTime? LastUserVerifiedDate { get; set; }
    public bool IsNotApplicable { get; set; }
    public string TextAnswer { get; set; }
    public int? CategoryTargetTypeId { get; set; }


    // Navigation Properties

    public CategoryTarget CategoryTarget { get; set; }
    public CategoryTargetStore CategoryTargetStore { get; set; }
    public Employee Employee { get; set; }
    public CategoryTargetType CategoryTargetType { get; set; }
}