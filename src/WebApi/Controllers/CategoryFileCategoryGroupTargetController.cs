using Engage.Application.Services.CategoryFileCategoryGroups.Commands;

namespace Engage.WebApi.Controllers
{
    public class CategoryFileCategoryGroupTargetController : BaseController
    {
        [HttpPost("bulkinsert")]
        public async Task<IActionResult> BulkInsert(CategoryFileCategoryGroupBulkInsertCommand command, CancellationToken cancellationToken)
        {
            var insertedIds = await Mediator.Send(command, cancellationToken);

            return insertedIds == null ? NotFound() : Ok(new OperationStatus(insertedIds.Count, command.CategoryFileId));
        }

        [HttpPut("bulkdelete")]
        public async Task<IActionResult> BulkDelete(CategoryFileCategoryGroupBulkDeleteCommand command, CancellationToken cancellationToken)
        {
            var deleteCount = await Mediator.Send(command, cancellationToken);

            return !deleteCount.HasValue ? NotFound() : Ok(new OperationStatus(deleteCount.Value, command.CategoryFileId));
        }
    }
}
