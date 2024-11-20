namespace Engage.Application.Services.EmployeeStoreCalendars.Queries;

public class EmployeeStoreCalendarAdvancedTargetingVm
{
    public bool HasAdvancedTargeting { get; set; }
    public List<int> Employees { get; set; }
    public List<int> EmployeeEngageRegions { get; set; }
    public List<int> EmployeeJobTitles { get; set; }
    public List<int> EmployeeEngageDepartments { get; set; }
    public List<int> EmployeeDivisions { get; set; }
    public ListResult<SurveyForm> SurveyForms { get; set; }
}
