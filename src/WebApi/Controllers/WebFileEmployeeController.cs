using Engage.Application.Services.WebFileEmployees.Commands;
using Engage.Application.Services.WebFileEmployees.Queries;

namespace Engage.WebApi.Controllers;

public partial class WebFileEmployeeController : BaseController
{
    [HttpGet()]
    public async Task<ActionResult<WebFileEmployeeTargeted>> Targeted([FromQuery] int employeeId, [FromQuery] DateTime date)
    {
        if (employeeId <= 0)
        {
            return BadRequest(BadIdText);
        }

        var result = await Mediator.Send(new WebFileEmployeeTargetedQuery(employeeId, date));

        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost("bulkinsert")]
    public async Task<IActionResult> BulkInsert(WebFileEmployeeBulkInsertCommand command, CancellationToken cancellationToken)
    {
        var insertedIds = await Mediator.Send(command, cancellationToken);

        return insertedIds == null ? NotFound() : Ok(new OperationStatus(insertedIds.Count, command.WebFileId));
    }

    [HttpPut("bulkdelete")]
    public async Task<IActionResult> BulkDelete(WebFileEmployeeBulkDeleteCommand command, CancellationToken cancellationToken)
    {
        var deleteCount = await Mediator.Send(command, cancellationToken);

        return !deleteCount.HasValue ? NotFound() : Ok(new OperationStatus(deleteCount.Value, command.WebFileId));
    }


}