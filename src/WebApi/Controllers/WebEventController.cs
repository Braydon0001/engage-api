using Engage.Application.Services.WebEvents.Models;
using Engage.Application.Services.WebEvents.Queries;
using Engage.Application.Services.WebEvents.Commands;

namespace Engage.WebApi.Controllers;

public class WebEventController : BaseController
{

    [HttpGet]
    public async Task<ActionResult<ListResult<WebEventDto>>> GetAll([FromRoute] WebEventQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("active")]
    public async Task<ActionResult<ListResult<WebEventDto>>> GetActive([FromRoute] WebEventActiveQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<WebEventVm>> GetVM([FromRoute] WebEventVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(WebEventCreateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(WebEventUpdateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
