// auto-generated
using Engage.Application.Services.EmployeeRecurringTransactionStatuses.Commands;
using Engage.Application.Services.EmployeeRecurringTransactionStatuses.Queries;

namespace Engage.WebApi.Controllers;

public partial class EmployeeRecurringTransactionStatusController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<EmployeeRecurringTransactionStatusDto>>> DtoList([FromQuery]EmployeeRecurringTransactionStatusListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<EmployeeRecurringTransactionStatusDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<EmployeeRecurringTransactionStatusOption>>> OptionList([FromQuery]EmployeeRecurringTransactionStatusOptionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeRecurringTransactionStatusVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new EmployeeRecurringTransactionStatusVmQuery { Id = id }, cancellationToken);
        if (entity == null)
        {
            return NotFound();
        }

        return entity;
    }

    [HttpPost]
    public async Task<IActionResult> Insert(EmployeeRecurringTransactionStatusInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.EmployeeRecurringTransactionStatusId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(EmployeeRecurringTransactionStatusUpdateCommand command, CancellationToken cancellationToken) 
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.EmployeeRecurringTransactionStatusId));
    }

}