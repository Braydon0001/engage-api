using Engage.Application.Services.WebFileEmployeeDivisions.Commands;

namespace Engage.WebApi.Controllers;

public partial class WebFileEmployeeDivisionController : BaseController
{
    [HttpPost("bulkinsert")]
    public async Task<IActionResult> BulkInsert(WebFileEmployeeDivisionBulkInsertCommand command, CancellationToken cancellationToken)
    {
        var insertIds = await Mediator.Send(command, cancellationToken);
        return insertIds == null ? NotFound() : Ok(new OperationStatus(insertIds.Count, command.WebFileId));
    }

    [HttpPut("bulkdelete")]
    public async Task<IActionResult> BulkDelete(WebFileEmployeeDivisionBulkDeleteCommand command, CancellationToken cancellationToken)
    {
        var deleteCount = await Mediator.Send(command, cancellationToken);
        return !deleteCount.HasValue ? NotFound() : Ok(new OperationStatus(deleteCount.Value, command.WebFileId));
    }
}
