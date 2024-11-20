namespace Engage.Domain.Entities;

public class SurveyFormEmployeeJobTitle : SurveyFormTarget
{
    public int EmployeeJobTitleId { get; set; }

    public EmployeeJobTitle EmployeeJobTitle { get; set; }
}
