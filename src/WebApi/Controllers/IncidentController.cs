using Engage.Application.Services.Incidents.Commands;
using Engage.Application.Services.Incidents.Models;
using Engage.Application.Services.Incidents.Queries;

namespace Engage.WebApi.Controllers;

public class IncidentController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<IncidentDto>>> GetAll([FromQuery] IncidentsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IncidentVm>> GetVm([FromRoute] IncidentVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateIncidentCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateIncidentCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
