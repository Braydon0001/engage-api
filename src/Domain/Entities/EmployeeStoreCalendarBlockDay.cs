// auto-generated
namespace Engage.Domain.Entities;

public class EmployeeStoreCalendarBlockDay : BaseAuditableEntity
{
    public int EmployeeStoreCalendarBlockDayId { get; set; }
    public int EmployeeId { get; set; }
    public int EmployeeStoreCalendarTypeId { get; set; }
    public int EmployeeStoreCalendarStatusId { get; set; }
    public DateTime CalendarDate { get; set; }
    public bool IsManagerCreated { get; set; }
    public int EmployeeStoreCalendarPeriodId { get; set; }
    public string Note { get; set; }

    // Navigation Properties

    public Employee Employee { get; set; }
    public EmployeeStoreCalendarType EmployeeStoreCalendarType { get; set; }
    public EmployeeStoreCalendarStatus EmployeeStoreCalendarStatus { get; set; }
    public EmployeeStoreCalendarPeriod EmployeeStoreCalendarPeriod { get; set; }
}