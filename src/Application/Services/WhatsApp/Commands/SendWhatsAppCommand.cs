namespace Engage.Application.Services.WhatsApp.Commands;

public class SendWhatsAppCommand : IRequest<OperationStatus>
{
    public List<string> ToMobileNumbers { get; set; }
    public string FromMobileNumber { get; set; }
    public string Message { get; set; }
    public object TemplateData { get; set; }
    public Dictionary<string, string> Parameters { get; set; }
    public string ExternalTemplateId { get; set; }
    public List<string> MediaUrls { get; set; } = null;
}

public class SendWhatsAppCommandHandler : IRequestHandler<SendWhatsAppCommand, OperationStatus>
{
    private readonly IHandlebarsService _handlebarsService;
    private readonly IWhatsAppService _whatsAppService;

    public SendWhatsAppCommandHandler(IHandlebarsService handlebarsService, IWhatsAppService whatsAppService)
    {
        _handlebarsService = handlebarsService;
        _whatsAppService = whatsAppService;
    }

    public async Task<OperationStatus> Handle(SendWhatsAppCommand command, CancellationToken cancellationToken)
    {
        var template = _handlebarsService.RenderTemplate(command.Message, command.TemplateData);

        var result = await _whatsAppService.SendMessageAsync(command.ToMobileNumbers,
                                                             command.FromMobileNumber,
                                                             template,
                                                             command.ExternalTemplateId,
                                                             command.MediaUrls,
                                                             command.Parameters,
                                                             cancellationToken);

        return result;
    }
}