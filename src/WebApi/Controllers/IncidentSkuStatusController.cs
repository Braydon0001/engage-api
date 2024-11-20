using Engage.Application.Services.IncidentSkuStatuses.Commands;
using Engage.Application.Services.IncidentSkuStatuses.Models;
using Engage.Application.Services.IncidentSkuStatuses.Queries;

namespace Engage.WebApi.Controllers;

public class IncidentSkuStatusController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<IncidentSkuStatusDto>>> GetAll([FromQuery] IncidentSkuStatusesQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IncidentSkuStatusVm>> GetVm([FromRoute] IncidentSkuStatusVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateIncidentSkuStatusCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateIncidentSkuStatusCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
