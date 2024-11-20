namespace Engage.Application.Models.Configuration
{
    public class SendGridOptions
    {
        public string ApiKey { get; set; }
        public string ClaimPaymentTemplateId { get; set; }
        public string ClaimRejectedTemplateId { get; set; }
        public string ClaimDisputedTemplateId { get; set; }
        public string ClaimApprovalReminderTemplateId { get; set; }
        public string FromEmailAddress { get; set; }
        public string FromName { get; set; }
    }
}
