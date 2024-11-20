namespace Engage.Application.Services.EmployeeStoreCalendars.Queries;

public static class EmployeeStoreCalendarPaginationProps
{
    public static Dictionary<string, PaginationProperty> Create()
    {
        return new()
        {
            {"id", new ("EmployeeStoreCalendarId") },
            { "employeeName", new ("Employee.FirstName") },
            { "employeeCode", new ("Employee.Code") },
            { "storeName", new ("Store.Name") },
            { "calendarDate", new ("CalendarDate") },
            { "employeeStoreCalendarPeriodName", new ("EmployeeStoreCalendarPeriod.name") }
        };
    }
}
