namespace Engage.Application.Services.Emails.Commands;

public class SendEmailCommand : IRequest<OperationStatus>
{
    public string ToEmailAddress { get; set; }
    public string FromEmailName { get; set; }
    public string FromEmailAddress { get; set; }
    public List<string> CcEmailAddresses { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public object TemplateData { get; set; }
    public List<string> AttachmentUrls { get; set; } = null;
    public MemoryStream AttachmentStream { get; set; } = null;
    public string AttachmentName { get; set; } = null;
    public string AttachmentContentType { get; set; } = null;
}

public class SendEmailCommandHandler : IRequestHandler<SendEmailCommand, OperationStatus>
{
    private readonly IHandlebarsService _handlebarsService;
    private readonly IEmailService2 _emailService2;

    public SendEmailCommandHandler(IHandlebarsService handlebarsService, IEmailService2 emailService2)
    {
        _handlebarsService = handlebarsService;
        _emailService2 = emailService2;
    }

    public async Task<OperationStatus> Handle(SendEmailCommand command, CancellationToken cancellationToken)
    {
        var subject = _handlebarsService.RenderTemplate(command.Subject, command.TemplateData);
        var template = _handlebarsService.RenderTemplate(command.Body, command.TemplateData);

        var result = await _emailService2.SendEmailAsync(command.ToEmailAddress,
                                                         command.CcEmailAddresses,
                                                         command.FromEmailAddress,
                                                         command.FromEmailName,
                                                         subject,
                                                         template,
                                                         cancellationToken,
                                                         command.AttachmentUrls,
                                                         command.AttachmentStream,
                                                         command.AttachmentName,
                                                         command.AttachmentContentType);

        return result;
    }
}