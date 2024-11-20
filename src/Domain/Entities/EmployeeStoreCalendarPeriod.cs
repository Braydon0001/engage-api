// auto-generated
namespace Engage.Domain.Entities;

public class EmployeeStoreCalendarPeriod : BaseAuditableEntity
{
    public int EmployeeStoreCalendarPeriodId { get; set; }
    public int EmployeeStoreCalendarYearId { get; set; }
    public string Name { get; set; }
    public int Number { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    // Navigation Properties

    public EmployeeStoreCalendarYear EmployeeStoreCalendarYear { get; set; }
}