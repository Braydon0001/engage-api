using Engage.Application.Services.IncidentStatuses.Commands;
using Engage.Application.Services.IncidentStatuses.Models;
using Engage.Application.Services.IncidentStatuses.Queries;

namespace Engage.WebApi.Controllers;

public class IncidentStatusController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<IncidentStatusDto>>> GetAll([FromQuery] IncidentStatusesQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IncidentStatusVm>> GetVm([FromRoute] IncidentStatusVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateIncidentStatusCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateIncidentStatusCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
