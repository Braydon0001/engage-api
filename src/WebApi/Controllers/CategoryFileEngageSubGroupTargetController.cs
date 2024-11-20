using Engage.Application.Services.CategoryFileEngageSubGroups.Commands;

namespace Engage.WebApi.Controllers
{
    public class CategoryFileEngageSubGroupTargetController : BaseController
    {
        [HttpPost("bulkinsert")]
        public async Task<IActionResult> BulkInsert(CategoryFileEngageSubGroupBulkInsertCommand command, CancellationToken cancellationToken)
        {
            var insertedIds = await Mediator.Send(command, cancellationToken);

            return insertedIds == null ? NotFound() : Ok(new OperationStatus(insertedIds.Count, command.CategoryFileId));
        }

        [HttpPut("bulkdelete")]
        public async Task<IActionResult> BulkDelete(CategoryFileEngageSubGroupBulkDeleteCommand command, CancellationToken cancellationToken)
        {
            var deleteCount = await Mediator.Send(command, cancellationToken);

            return !deleteCount.HasValue ? NotFound() : Ok(new OperationStatus(deleteCount.Value, command.CategoryFileId));
        }
    }
}
