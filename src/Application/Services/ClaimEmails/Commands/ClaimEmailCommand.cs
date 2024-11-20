namespace Engage.Application.Services.ClaimEmails.Commands
{
    public class ClaimEmailCommand
    {
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string Subject { get; set; }
        public string ClaimNumber { get; set; }
        public List<string> CcEmailAddresses { get; set; }
    }
}
