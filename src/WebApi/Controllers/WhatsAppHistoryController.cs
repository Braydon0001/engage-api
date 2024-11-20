using Engage.Application.Services.WhatsAppHistories.Queries;

namespace Engage.WebApi.Controllers;

[Authorize("store")]
public class WhatsAppHistoryController : BaseController
{
    [HttpPost("page")]
    public async Task<ActionResult<ListResult<WhatsAppHistoryDto>>> Paginated(WhatsAppHistoryPaginatedQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<WhatsAppHistoryDto>(entities));
    }
}
