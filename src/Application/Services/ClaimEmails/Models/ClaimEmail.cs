namespace Engage.Application.Services.ClaimEmails.Models
{
    public class ClaimEmail
    {
        public ClaimEmail()
        {
            StoreClaimPaymentTemplateProps = new StoreClaimPaymentTemplateProps();
            RejectedClaimTemplateProps = new RejectedClaimTemplateProps();
            DisputedClaimTemplateProps = new DisputedClaimTemplateProps();
            ClaimApprovalReminderTemplateProps = new ClaimApprovalReminderTemplateProps();
        }
        public EmailTypeId ClaimEmailTypeId { get; set; }
        public string EmailAddress { get; set; }
        public string Subject { get; set; }
        public List<string> CcEmailAddresses { get; set; }
        public StoreClaimPaymentTemplateProps StoreClaimPaymentTemplateProps { get; set; }
        public RejectedClaimTemplateProps RejectedClaimTemplateProps { get; set; }
        public DisputedClaimTemplateProps DisputedClaimTemplateProps { get; set; }
        public ClaimApprovalReminderTemplateProps ClaimApprovalReminderTemplateProps { get; set; }
    }
}
