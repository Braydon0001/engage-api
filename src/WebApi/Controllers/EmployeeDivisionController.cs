using Engage.Application.Services.EmployeeDivisions.Commands;
using Engage.Application.Services.EmployeeDivisions.Queries;

namespace Engage.WebApi.Controllers;

public partial class EmployeeDivisionController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<EmployeeDivisionDto>>> DtoList([FromQuery] EmployeeDivisionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<EmployeeDivisionDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<EmployeeDivisionOption>>> OptionList([FromQuery] EmployeeDivisionOptionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeDivisionVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new EmployeeDivisionVmQuery { Id = id }, cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(EmployeeDivisionInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.EmployeeDivisionId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(EmployeeDivisionUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.EmployeeDivisionId));
    }
}