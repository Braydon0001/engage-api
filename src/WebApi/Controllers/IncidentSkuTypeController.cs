using Engage.Application.Services.IncidentSkuTypes.Commands;
using Engage.Application.Services.IncidentSkuTypes.Models;
using Engage.Application.Services.IncidentSkuTypes.Queries;

namespace Engage.WebApi.Controllers;

public class IncidentSkuTypeController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<IncidentSkuTypeDto>>> GetAll([FromQuery] IncidentSkuTypesQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IncidentSkuTypeVm>> GetVm([FromRoute] IncidentSkuTypeVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateIncidentSkuTypeCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateIncidentSkuTypeCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
