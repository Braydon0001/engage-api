namespace Engage.Domain.Entities;

public class EmployeeStoreCalendarSurveyFormSubmission : BaseAuditableEntity
{
    public int EmployeeStoreCalendarSurveyFormSubmissionId { get; set; }
    public int EmployeeStoreCalendarId { get; set; }
    public int SurveyFormSubmissionId { get; set; }

    // Navigation Properties

    public EmployeeStoreCalendar EmployeeStoreCalendar { get; set; }
    public SurveyFormSubmission SurveyFormSubmission { get; set; }
}