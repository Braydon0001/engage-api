// auto-generated
using Engage.Application.Services.EmployeeTransactionStatuses.Commands;
using Engage.Application.Services.EmployeeTransactionStatuses.Queries;

namespace Engage.WebApi.Controllers;

public partial class EmployeeTransactionStatusController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<EmployeeTransactionStatusDto>>> DtoList([FromQuery]EmployeeTransactionStatusListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<EmployeeTransactionStatusDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<EmployeeTransactionStatusOption>>> OptionList([FromQuery]EmployeeTransactionStatusOptionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeTransactionStatusVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new EmployeeTransactionStatusVmQuery { Id = id }, cancellationToken);
        if (entity == null)
        {
            return NotFound();
        }

        return entity;
    }

    [HttpPost]
    public async Task<IActionResult> Insert(EmployeeTransactionStatusInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.EmployeeTransactionStatusId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(EmployeeTransactionStatusUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.EmployeeTransactionStatusId));
    }
}