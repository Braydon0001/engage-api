using Engage.Application.Services.ClaimStatuses.Queries;

namespace Engage.WebApi.Controllers;

public class ClaimStatusController : BaseController
{
    [HttpGet("option")]
    public async Task<ActionResult<List<OptionDto>>> GetOptions([FromQuery] ClaimStatusOptionsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
}
