namespace Engage.Application.Interfaces;

public interface IWhatsAppService
{
    Task<OperationStatus> SendMessageAsync(
        List<string> toMobileNumbers,
        string fromMobileNumber,
        string message,
        string externalTemplateId,
        List<string> mediaUrls = null,
        Dictionary<string, string> parameters = null,
        CancellationToken cancellationToken = default);
}
