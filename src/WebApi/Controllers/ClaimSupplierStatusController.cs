using Engage.Application.Services.ClaimSupplierStatuses.Queries;

namespace Engage.WebApi.Controllers;

public class ClaimSupplierStatusController : BaseController
{
    [HttpGet("option")]
    public async Task<ActionResult<List<OptionDto>>> GetOptions([FromQuery] ClaimSupplierStatusOptionsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
}
