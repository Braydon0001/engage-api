namespace Engage.Application.Services.SurveyFormSubmissions.Commands;

public class SurveyFormNotificationEmailTemplate
{
    [JsonProperty("employeeName")]
    public string EmployeeName { get; set; }

    [JsonProperty("storeName")]
    public string StoreName { get; set; }

    [JsonProperty("submissionId")]
    public int SubmissionId { get; set; }

    [JsonProperty("requestDate")]
    public string RequestDate { get; set; }

    [JsonProperty("dcAccountNumber")]
    public string DcAccountNumber { get; set; }
}


