namespace Engage.Application.Services.Emails.Commands;

public class SendSmtpEmailCommand : IRequest<OperationStatus>
{
    public string ToEmailAddress { get; set; }
    public string FromEmailAddress { get; set; }
    public List<string> CcEmailAddresses { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public List<string> AttachmentUrls { get; set; } = null;
    public MemoryStream AttachmentStream { get; set; } = null;
    public string AttachmentName { get; set; } = null;
    public string AttachmentContentType { get; set; } = null;
}

public class SendSmtpEmailCommandHandler : IRequestHandler<SendSmtpEmailCommand, OperationStatus>
{
    private readonly IEmailService2 _emailService2;

    public SendSmtpEmailCommandHandler(IEmailService2 emailService2)
    {
        _emailService2 = emailService2;
    }

    public async Task<OperationStatus> Handle(SendSmtpEmailCommand command, CancellationToken cancellationToken)
    {
        var result = await _emailService2.SendSmtpEmailAsync(command.ToEmailAddress,
                                                             command.CcEmailAddresses,
                                                             command.FromEmailAddress,
                                                             command.Subject,
                                                             command.Body,
                                                             cancellationToken,
                                                             command.AttachmentUrls,
                                                             command.AttachmentStream,
                                                             command.AttachmentName,
                                                             command.AttachmentContentType);

        return result;
    }
}