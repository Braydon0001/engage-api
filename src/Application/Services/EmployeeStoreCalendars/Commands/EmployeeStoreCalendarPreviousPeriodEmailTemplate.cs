namespace Engage.Application.Services.EmployeeStoreCalendars.Commands;

public class EmployeeStoreCalendarPreviousPeriodEmailTemplate
{
    [JsonProperty("name")]
    public string Name { get; set; }
    public int EmployeeId { get; set; }
    public DateTime ReportDate { get; set; }
}
