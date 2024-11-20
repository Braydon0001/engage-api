namespace Engage.Application.Services.ClaimEmails.Models
{
    public class ClaimApprovalReminderTemplateProps : TemplateProps
    {
        [JsonProperty("cutOffDate")]
        public string CutOffDate { get; set; }
    }
}
