using Engage.Application.Services.ClaimTypeReportTypes.Commands;
using Engage.Application.Services.ClaimTypeReportTypes.Models;
using Engage.Application.Services.ClaimTypeReportTypes.Queries;

namespace Engage.WebApi.Controllers;

public class ClaimTypeReportTypeController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<PaginatedListResult<ClaimTypeReportTypeDto>>> GetAll([FromQuery] ClaimTypeReportTypesQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost()]
    public async Task<IActionResult> Create(CreateClaimTypeReportTypesCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPost("delete")]
    public async Task<IActionResult> Delete(DeleteClaimTypeReportTypesCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
