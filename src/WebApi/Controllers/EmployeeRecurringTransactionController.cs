// auto-generated
using Engage.Application.Services.EmployeeRecurringTransactions.Commands;
using Engage.Application.Services.EmployeeRecurringTransactions.Queries;

namespace Engage.WebApi.Controllers;

public partial class EmployeeRecurringTransactionController : BaseController
{
    [HttpGet("page")]
    public async Task<ActionResult<ListResult<EmployeeRecurringTransactionDto>>> PaginatedList([FromQuery] EmployeeRecurringTransactionPaginatedListQuery query, CancellationToken cancellationToken)
    {
      var entities = await Mediator.Send(query, cancellationToken);

      return Ok(new ListResult<EmployeeRecurringTransactionDto>(entities));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeRecurringTransactionVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new EmployeeRecurringTransactionVmQuery { Id = id }, cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(EmployeeRecurringTransactionInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.EmployeeRecurringTransactionId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(EmployeeRecurringTransactionUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.EmployeeRecurringTransactionId));
    }


}