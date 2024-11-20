namespace Engage.Application.Services.Employees.Models;

public class TerminationEmailTemplate
{
    [JsonProperty("name")]
    public string Name { get; set; }
    [JsonProperty("employeeName")]
    public string EmployeeName { get; set; }
    [JsonProperty("terminationReason")]
    public string TerminationReason { get; set; }
    [JsonProperty("terminationDate")]
    public string TerminationDate { get; set; }
    [JsonProperty("terminatedBy")]
    public string TerminatedBy { get; set; }
}
