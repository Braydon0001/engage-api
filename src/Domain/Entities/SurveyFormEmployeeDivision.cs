namespace Engage.Domain.Entities;

public class SurveyFormEmployeeDivision : SurveyFormTarget
{
    public int EmployeeDivisionId { get; set; }
    public EmployeeDivision EmployeeDivision { get; set; }
}
