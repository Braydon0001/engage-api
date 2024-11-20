namespace Engage.Domain.Enums;

public enum EmailTypeId
{
    ClaimPayment = 1,
    ClaimRejected,
    ClaimDisputed,
    ClaimApprovalReminder,
    ClaimFloatWarning,
    EmployeeTermination,
    EmployeeStoreCalendarCurrentPeriodReport,
    EmployeeStoreCalendarPreviousPeriodReport,
    ContactReportComplete,
    OrderSubmit,
    StoreVisitEventCreated,
    SurveyFormPOSUpdate = 12
}
