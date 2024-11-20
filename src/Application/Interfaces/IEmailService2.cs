namespace Engage.Application.Interfaces;

public interface IEmailService2
{
    Task<OperationStatus> SendEmailAsync(
        string toEmailAddress,
        List<string> ccEmailAddresses,
        string fromEmailAddress,
        string senderName,
        string subject,
        string htmlContent,
        CancellationToken cancellationToken,
        List<string> attachmentUrls = null,
        MemoryStream attachmentStream = null,
        string attachmentName = null,
        string attachmentContentType = null);

    Task<OperationStatus> SendSmtpEmailAsync(
        string toEmailAddress,
        List<string> ccEmailAddresses,
        string fromEmailAddress,
        string subject,
        string htmlContent,
        CancellationToken cancellationToken,
        List<string> attachmentUrls = null,
        MemoryStream attachmentStream = null,
        string attachmentName = null,
        string attachmentContentType = null);
}
