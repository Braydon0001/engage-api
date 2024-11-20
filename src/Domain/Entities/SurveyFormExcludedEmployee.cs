namespace Engage.Domain.Entities;

public class SurveyFormExcludedEmployee : SurveyFormTarget
{
    public int ExcludedEmployeeId { get; set; }
    public Employee ExcludedEmployee { get; set; }
}
