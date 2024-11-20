using Engage.Application.Services.CommunicationHistories.Commands;
using Engage.Application.Services.CommunicationHistories.Queries;

namespace Engage.WebApi.Controllers;

[Authorize("store")]
public class CommunicationHistoryController : BaseController
{
    [HttpPost("page")]
    public async Task<ActionResult<ListResult<CommunicationHistoryDto>>> Paginated(CommunicationHistoryPaginatedQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<CommunicationHistoryDto>(entities));
    }

    [HttpPost("resendemails")]
    public async Task<IActionResult> ResendEmails(ResendEmailsCommand command, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(command, cancellationToken));
    }
}
