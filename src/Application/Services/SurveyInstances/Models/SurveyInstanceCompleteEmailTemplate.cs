namespace Engage.Application.Services.SurveyInstances.Models;

public class SurveyInstanceCompleteEmailTemplate
{
    [JsonProperty("name")]
    public string Name { get; set; }
    [JsonProperty("storeName")]
    public string StoreName { get; set; }
    public int? SurveyInstanceId { get; set; }
    public string ReportDate { get; set; }
}
