// auto-generated
namespace Engage.Domain.Entities;

public class EmployeePension : BaseAuditableEntity
{
    public int EmployeePensionId { get; set; }
    public int EmployeeId { get; set; }
    public int EmployeePensionSchemeId { get; set; }
    public int EmployeePensionCategoryId { get; set; }
    public int EmployeePensionContributionPercentageId { get; set; }
    public List<JsonFile> Files { get; set; }

    // Navigation Properties

    public Employee Employee { get; set; }
    public EmployeePensionScheme EmployeePensionScheme { get; set; }
    public EmployeePensionCategory EmployeePensionCategory { get; set; }
    public EmployeePensionContributionPercentage EmployeePensionContributionPercentage { get; set; }
}