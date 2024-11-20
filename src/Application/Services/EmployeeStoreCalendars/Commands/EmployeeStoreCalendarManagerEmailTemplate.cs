namespace Engage.Application.Services.EmployeeStoreCalendars.Commands;

public class EmployeeStoreCalendarManagerEmailTemplate
{
    public int EmployeeStoreCalendarId { get; set; }
    [JsonProperty("name")]
    public string Name { get; set; }
    [JsonProperty("storeName")]
    public string StoreName { get; set; }
    [JsonProperty("calendarDate")]
    public string CalendarDate { get; set; }
}
