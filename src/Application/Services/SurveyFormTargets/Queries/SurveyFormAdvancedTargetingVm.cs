using Engage.Application.Services.Mobile.SurveyForms.Queries;

public class SurveyFormAdvancedTargetingVm
{
    public bool HasAdvancedTargeting { get; set; }
    public List<int> Employees { get; set; }
    public List<int> EmployeeEngageRegions { get; set; }
    public List<int> EmployeeJobTitles { get; set; }
    public List<int> EmployeeEngageDepartments { get; set; }
    public List<int> EmployeeDivisions { get; set; }
    public List<int> Stores { get; set; }
    public List<int> StoreEngageRegions { get; set; }
    public List<int> StoreFormats { get; set; }
    public List<int> StoreClusters { get; set; }
    public List<int> StoreLSMs { get; set; }
    public List<int> StoreTypes { get; set; }
    public ListResult<SurveyFormWithQuestions> SurveyForms { get; set; }
}
