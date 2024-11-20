using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net.Mail;
using System.Text;

namespace Engage.Infrastructure.Services;

public class EmailService2 : IEmailService2
{
    private readonly SendGridOptions _sendGridOptions;
    private readonly SmtpSettings _smtpSettings;

    public EmailService2(IOptions<SendGridOptions> sendGridSettings, IOptions<SmtpSettings> smtpSettings)
    {
        _sendGridOptions = sendGridSettings.Value;
        _smtpSettings = smtpSettings.Value;
    }
    public async Task<OperationStatus> SendEmailAsync(string toEmailAddress, List<string> ccEmailAddresses, string fromEmailAddress, string senderName, string subject, string htmlContent, CancellationToken cancellationToken, List<string> attachmentUrls = null, MemoryStream attachmentStream = null, string attachmentName = null, string attachmentContentType = null)
    {
        var client = new SendGridClient(_sendGridOptions.ApiKey);

        var message = new SendGridMessage
        {
            From = new EmailAddress(fromEmailAddress, senderName),
            Subject = subject,
            HtmlContent = htmlContent
        };

        message.AddTo(new EmailAddress(toEmailAddress));

        if (ccEmailAddresses != null && ccEmailAddresses.Count > 0)
        {
            foreach (var emailAddress in ccEmailAddresses)
            {
                message.AddBcc(emailAddress.Trim());
            }
        }

        if (attachmentUrls != null && attachmentUrls.Count > 0)
        {
            foreach (var url in attachmentUrls)
            {
                var urlAttachment = await GetAttachmentFromUrlAsync(url);
                if (urlAttachment != null)
                {
                    message.AddAttachment(urlAttachment);
                }
            }
        }

        if (attachmentStream != null && !string.IsNullOrEmpty(attachmentName) && !string.IsNullOrEmpty(attachmentContentType))
        {
            var attachmentBytes = attachmentStream.ToArray();
            var attachment = new SendGrid.Helpers.Mail.Attachment
            {
                Content = Convert.ToBase64String(attachmentBytes),
                Filename = attachmentName,
                Type = attachmentContentType
            };
            message.AddAttachment(attachment);
        }

        var response = await client.SendEmailAsync(message, cancellationToken);

        return new OperationStatus
        {
            Status = response.IsSuccessStatusCode
        };
    }

    public async Task<OperationStatus> SendSmtpEmailAsync(string toEmailAddress, List<string> ccEmailAddresses, string fromEmailAddress, string subject, string htmlContent, CancellationToken cancellationToken, List<string> attachmentUrls = null, MemoryStream attachmentStream = null, string attachmentName = null, string attachmentContentType = null)
    {
        var smtp = new SmtpClient(_smtpSettings.SmtpHost)
        {
            Port = _smtpSettings.SmtpPort,
            Credentials = new System.Net.NetworkCredential(_smtpSettings.SmtpName, _smtpSettings.SmtpPassword),
            EnableSsl = true
        };

        var message = new MailMessage
        {
            From = new MailAddress(_smtpSettings.SmtpName),
            Subject = subject,
            Body = htmlContent,
            IsBodyHtml = true
        };
        message.To.Add(new MailAddress(toEmailAddress.Trim()));

        if (ccEmailAddresses != null && ccEmailAddresses.Count > 0)
        {
            foreach (var emailAddress in ccEmailAddresses)
            {
                message.Bcc.Add(new MailAddress(emailAddress.Trim()));
            }
        }

        try
        {
            if (attachmentUrls != null && attachmentUrls.Any())
            {
                var stringBuilder = new StringBuilder(htmlContent);
                stringBuilder.Append("<br/><br/><strong>Attachments:</strong><br/>");

                foreach (var url in attachmentUrls)
                {
                    var fileName = Path.GetFileName(new Uri(url).AbsolutePath);
                    stringBuilder.AppendFormat("<a href=\"{0}\">{1}</a><br/>", url, fileName);
                }

                message.Body = stringBuilder.ToString();
            }

            if (attachmentStream != null && !string.IsNullOrEmpty(attachmentName) && !string.IsNullOrEmpty(attachmentContentType))
            {
                message.Attachments.Add(new System.Net.Mail.Attachment(attachmentStream, attachmentName, attachmentContentType));
            }

            await smtp.SendMailAsync(message, cancellationToken);
            return new OperationStatus { Status = true };
        }
        catch (Exception ex)
        {
            return new OperationStatus
            {
                Status = false,
                Message = "Error Sending Email.",
                ExceptionMessage = ex.Message,
                ExceptionInnerMessage = ex.InnerException?.Message,
                ExceptionStackTrace = ex.StackTrace,
            };
        }

    }


    private async Task<SendGrid.Helpers.Mail.Attachment> GetAttachmentFromUrlAsync(string url)
    {
        using (var httpClient = new HttpClient())
        {
            var response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsByteArrayAsync();
                var fileName = new Uri(url).Segments.Last();

                return new SendGrid.Helpers.Mail.Attachment
                {
                    Content = Convert.ToBase64String(content),
                    Filename = fileName,
                    Type = response.Content.Headers.ContentType.ToString(),
                    Disposition = "attachment"
                };
            }
        }

        return null;
    }
}
