// auto-generated
using Engage.Application.Services.CategoryFileEngageRegions.Commands;

namespace Engage.WebApi.Controllers;

public partial class CategoryFileEngageRegionController : BaseController
{
    [HttpPost("bulkinsert")]
    public async Task<IActionResult> BulkInsert(CategoryFileEngageRegionBulkInsertCommand command, CancellationToken cancellationToken)
    {
        var insertedIds = await Mediator.Send(command, cancellationToken);

        return insertedIds == null ? NotFound() : Ok(new OperationStatus(insertedIds.Count, command.CategoryFileId));
    }

    [HttpPut("bulkdelete")]
    public async Task<IActionResult> BulkDelete(CategoryFileEngageRegionBulkDeleteCommand command, CancellationToken cancellationToken)
    {
        var deleteCount = await Mediator.Send(command, cancellationToken);

        return !deleteCount.HasValue ? NotFound() : Ok(new OperationStatus(deleteCount.Value, command.CategoryFileId));
    }


}