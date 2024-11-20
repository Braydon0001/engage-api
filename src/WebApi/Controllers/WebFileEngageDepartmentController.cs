using Engage.Application.Services.WebFileEngageDepartments.Commands;

namespace Engage.WebApi.Controllers;

public partial class WebFileEngageDepartmentController : BaseController
{
    [HttpPost("bulkinsert")]
    public async Task<IActionResult> BulkInsert(WebFileEngageDepartmentBulkInsertCommand command, CancellationToken cancellationToken)
    {
        var insertIds = await Mediator.Send(command, cancellationToken);
        return insertIds == null ? NotFound() : Ok(new OperationStatus(insertIds.Count, command.WebFileId));
    }

    [HttpPut("bulkdelete")]
    public async Task<IActionResult> BulkDelete(WebFileEngageDepartmentBulkDeleteCommand command, CancellationToken cancellationToken)
    {
        var deleteCount = await Mediator.Send(command, cancellationToken);
        return !deleteCount.HasValue ? NotFound() : Ok(new OperationStatus(deleteCount.Value, command.WebFileId));
    }
}
