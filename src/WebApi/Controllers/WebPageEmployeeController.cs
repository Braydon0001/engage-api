// auto-generated
using Engage.Application.Services.WebPageEmployees.Commands;
using Engage.Application.Services.WebPageEmployees.Queries;

namespace Engage.WebApi.Controllers;

public partial class WebPageEmployeeController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<WebPageEmployeeDto>>> DtoList([FromQuery] WebPageEmployeeListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<WebPageEmployeeDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<WebPageEmployeeOption>>> OptionList([FromQuery] WebPageEmployeeOptionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<WebPageEmployeeVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new WebPageEmployeeVmQuery { Id = id }, cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(WebPageEmployeeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.WebPageEmployeeId));
    }

    [HttpPost("log")]
    public async Task<IActionResult> Log(WebPageEmployeeLogCommand command, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(command, cancellationToken));
    }

    [HttpPut]
    public async Task<IActionResult> Update(WebPageEmployeeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.WebPageEmployeeId));
    }


}