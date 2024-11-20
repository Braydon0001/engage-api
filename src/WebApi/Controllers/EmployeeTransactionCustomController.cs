using Engage.Application.Services.EmployeeTransactions.Commands;
using Engage.Application.Services.EmployeeTransactions.Queries;

namespace Engage.WebApi.Controllers;

public partial class EmployeeTransactionController : BaseController
{
    [HttpGet("transactiontypegroupId/{transactionTypeGroupId}/employeeId/{employeeid}")]
    [HttpGet("transactiontypegroupId/{transactionTypeGroupId}")]
    [HttpGet("transactiontypeid/{transactionTypeId}/employeeId/{employeeid}")]
    [HttpGet("transactiontypeid/{transactionTypeId}")]
    [HttpGet("engageregionid/{engageRegionId}/transactiontypeid/{transactionTypeId}/payrollperiodid/{payrollPeriodId}/transactionstatusid/{transactionStatusId}")]
    public async Task<ActionResult<ListResult<EmployeeTransactionDto>>> DtoList([FromRoute] EmployeeTransactionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<EmployeeTransactionDto>(entities));
    }

    [HttpGet("allowances/employeeId/{employeeid}")]
    public async Task<ActionResult<ListResult<EmployeeTransactionDto>>> OnceOffAllowancesList([FromRoute] EmployeeTransactionListQuery query, CancellationToken cancellationToken)
    {
        //query.IsRecurring = false;
        //query.IsPositive = true;
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<EmployeeTransactionDto>(entities));
    }

    [HttpGet("deductions/employeeId/{employeeid}")]
    public async Task<ActionResult<ListResult<EmployeeTransactionDto>>> OnceOffDeductionsList([FromRoute] EmployeeTransactionListQuery query, CancellationToken cancellationToken)
    {
        //query.IsRecurring = false;
        //query.IsPositive = false;
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<EmployeeTransactionDto>(entities));
    }

    [HttpGet("allowancesrecurring/employeeId/{employeeid}")]
    public async Task<ActionResult<ListResult<EmployeeTransactionDto>>> RecurringAllowancesList([FromRoute] EmployeeTransactionListQuery query, CancellationToken cancellationToken)
    {
        //query.IsRecurring = true;
        //query.IsPositive = true;
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<EmployeeTransactionDto>(entities));
    }

    [HttpGet("deductionsrecurring/employeeId/{employeeid}")]
    public async Task<ActionResult<ListResult<EmployeeTransactionDto>>> RecurringDeductionsList([FromRoute] EmployeeTransactionListQuery query, CancellationToken cancellationToken)
    {
        //query.IsRecurring = true;
        //query.IsPositive = false;
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<EmployeeTransactionDto>(entities));
    }

    [HttpPost]
    public async Task<IActionResult> InsertAllowance(EmployeeTransactionInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.EmployeeTransactionId));
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAllowance(EmployeeTransactionUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.EmployeeTransactionId));
    }

    [HttpPut("bulk/hours")]
    public async Task<IActionResult> BulkUpdateHours(Dictionary<string, EmployeeHour> updates, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new EmployeeTransactionBulkUpdateHoursCommand(updates), cancellationToken));
    }

    [HttpPut("bulk/groupid/{transactiontypegroupid}")]
    public async Task<IActionResult> BulkUpdate([FromBody] List<EmployeeTransactionUpdateCommand> updates, [FromRoute] int transactionTypeGroupId, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new EmployeeTransactionBulkUpdateCommand(updates), cancellationToken));
    }

    [HttpPut("batch/transactionstatus")]
    public async Task<IActionResult> BatchUpdateClaimStatus(BatchUpdateEmployeeTransactionStatusCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{id}")]
    public override async Task<IActionResult> Delete([FromRoute] int id)
    {
        return Ok(await Mediator.Send(new EmployeeTransactionDeleteCommand
        {
            Id = id
        }));
    }
}