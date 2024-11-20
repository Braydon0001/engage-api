// auto-generated

using Engage.Application.Services.CategoryFileStoreFormats.Commands;

namespace Engage.WebApi.Controllers;

public partial class CategoryFileStoreFormatTargetController : BaseController
{
    [HttpPost("bulkinsert")]
    public async Task<IActionResult> BulkInsert(CategoryFileStoreFormatBulkInsertCommand command, CancellationToken cancellationToken)
    {
        var insertedIds = await Mediator.Send(command, cancellationToken);

        return insertedIds == null ? NotFound() : Ok(new OperationStatus(insertedIds.Count, command.CategoryFileId));
    }

    [HttpPut("bulkdelete")]
    public async Task<IActionResult> BulkDelete(CategoryFileStoreFormatBulkDeleteCommand command, CancellationToken cancellationToken)
    {
        var deleteCount = await Mediator.Send(command, cancellationToken);

        return !deleteCount.HasValue ? NotFound() : Ok(new OperationStatus(deleteCount.Value, command.CategoryFileId));
    }
}