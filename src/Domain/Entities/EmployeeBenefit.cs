namespace Engage.Domain.Entities;

public class EmployeeBenefit : BaseAuditableEntity
{
    public int EmployeeBenefitId { get; set; }
    public int EmployeeId { get; set; }
    public int BenefitTypeId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public float Value { get; set; }
    public DateTime IssuedDate { get; set; }

    // Navigation Properties
    public Employee Employee { get; set; }
    public BenefitType BenefitType { get; set; }
}
