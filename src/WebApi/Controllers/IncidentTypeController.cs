using Engage.Application.Services.IncidentTypes.Commands;
using Engage.Application.Services.IncidentTypes.Models;
using Engage.Application.Services.IncidentTypes.Queries;

namespace Engage.WebApi.Controllers;

public class IncidentTypeController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<IncidentTypeDto>>> GetAll([FromQuery] IncidentTypesQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("option")]
    public async Task<ActionResult<List<OptionDto>>> GetOptions([FromQuery] IncidentTypeOptionsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<IncidentTypeVm>> GetVm([FromRoute] IncidentTypeVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateIncidentTypeCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateIncidentTypeCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
