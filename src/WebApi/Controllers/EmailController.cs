using Engage.Application.Services.Emails.Commands;

namespace Engage.WebApi.Controllers;

public partial class EmailController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> SendEmail(SendEmailCommand command, CancellationToken cancellationToken)
    {
        var opStatus = await Mediator.Send(command, cancellationToken);

        return Ok(opStatus);
    }
}
