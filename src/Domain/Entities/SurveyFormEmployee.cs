namespace Engage.Domain.Entities;

public class SurveyFormEmployee : SurveyFormTarget
{
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; }
}
