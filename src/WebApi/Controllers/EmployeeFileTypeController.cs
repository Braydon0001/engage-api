using Engage.Application.Services.EmployeeFileTypes.Commands;
using Engage.Application.Services.EmployeeFileTypes.Queries;

namespace Engage.WebApi.Controllers;

public partial class EmployeeFileTypeController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<EmployeeFileTypeDto>>> DtoList([FromQuery] EmployeeFileTypeListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<EmployeeFileTypeDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<EmployeeFileTypeOption>>> OptionList([FromQuery] EmployeeFileTypeOptionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeFileTypeVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new EmployeeFileTypeVmQuery { Id = id }, cancellationToken);
        if (entity == null)
        {
            return NotFound();
        }

        return entity;
    }

    [HttpPost]
    public async Task<IActionResult> Insert(EmployeeFileTypeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.EmployeeFileTypeId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(EmployeeFileTypeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.EmployeeFileTypeId));
    }
}