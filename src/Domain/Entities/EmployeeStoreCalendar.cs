// auto-generated
namespace Engage.Domain.Entities;

public class EmployeeStoreCalendar : BaseAuditableEntity
{
    public EmployeeStoreCalendar()
    {
        SurveyFormSubmissions = new HashSet<EmployeeStoreCalendarSurveyFormSubmission>();
    }
    public int EmployeeStoreCalendarId { get; set; }
    public int EmployeeId { get; set; }
    public int StoreId { get; set; }
    public int? EmployeeStoreCalendarTypeId { get; set; }
    public int? EmployeeStoreCalendarStatusId { get; set; }
    public bool IsManagerCreated { get; set; }
    public DateTime CalendarDate { get; set; }
    public int? Order { get; set; }
    public int EmployeeStoreCalendarPeriodId { get; set; }
    public int EmployeeStoreCalendarGroupId { get; set; }
    public int? SurveyInstanceId { get; set; }
    public DateTime? CompletionDate { get; set; }
    public string Note { get; set; }
    public string EmailedTo { get; set; }

    // Navigation Properties

    public Employee Employee { get; set; }
    public Store Store { get; set; }
    public EmployeeStoreCalendarType EmployeeStoreCalendarType { get; set; }
    public EmployeeStoreCalendarStatus EmployeeStoreCalendarStatus { get; set; }
    public EmployeeStoreCalendarPeriod EmployeeStoreCalendarPeriod { get; set; }
    public EmployeeStoreCalendarGroup EmployeeStoreCalendarGroup { get; set; }
    public SurveyInstance SurveyInstance { get; set; }
    public ICollection<EmployeeStoreCalendarSurveyFormSubmission> SurveyFormSubmissions { get; private set; }
}