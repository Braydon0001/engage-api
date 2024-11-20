
namespace Engage.Application.Services.EmployeeStoreCalendars.Commands;

public class EmployeeStoreCalendarCurrentPeriodEmailTemplate
{
    [JsonProperty("name")]
    public string Name { get; set; }
    public int EmployeeId { get; set; }
    public DateTime ReportDate { get; set; }
}
