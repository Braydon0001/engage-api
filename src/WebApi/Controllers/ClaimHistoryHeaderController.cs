using Engage.Application.Services.ClaimBatches.Commands;
using Engage.Application.Services.ClaimBatches.Models;
using Engage.Application.Services.ClaimBatches.Queries;

namespace Engage.WebApi.Controllers;

[Authorize("claim")]
public class ClaimHistoryHeaderController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<ClaimHistoryHeaderDto>>> GetAllByClaim([FromQuery] ClaimHistoryHeadersQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateClaimHistoryHeaderCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
