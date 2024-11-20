using Engage.Application.Services.IncidentSkus.Commands;
using Engage.Application.Services.IncidentSkus.Models;
using Engage.Application.Services.IncidentSkus.Queries;

namespace Engage.WebApi.Controllers;

public class IncidentSkuController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<IncidentSkuDto>>> GetAll([FromQuery] IncidentSkusQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IncidentSkuVm>> GetVm([FromRoute] IncidentSkuVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateIncidentSkuCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateIncidentSkuCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
