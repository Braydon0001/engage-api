using Engage.Application.Services.ClaimHistories.Commands;
using Engage.Application.Services.ClaimHistories.Models;
using Engage.Application.Services.ClaimHistories.Queries;

namespace Engage.WebApi.Controllers;

[Authorize("claim")]
public class ClaimHistoryController : BaseController
{
    [HttpGet("claim/{claimid}")]
    public async Task<ActionResult<ListResult<ClaimHistoryDto>>> GetAllByClaim([FromQuery] ClaimHistoriesQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateClaimHistoryCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
