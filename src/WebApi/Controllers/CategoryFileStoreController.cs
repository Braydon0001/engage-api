using Engage.Application.Services.CategoryFiles.Queries;
using Engage.Application.Services.CategoryFileStores.Commands;
using Engage.Application.Services.CategoryFileStores.Queries;

namespace Engage.WebApi.Controllers
{
    public class CategoryFileStoreController : BaseController
    {

        [HttpGet()]
        public async Task<ActionResult<ListResult<CategoryFileDto>>> Targeted([FromQuery] int storeId, [FromQuery] DateTime date)
        {
            if (storeId <= 0)
            {
                return BadRequest(BadIdText);
            }



            var result = await Mediator.Send(new CategoryFileStoreTargetedQuery(storeId, date));

            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost("bulkinsert")]
        public async Task<IActionResult> BulkInsert(CategoryFileStoreBulkInsertCommand command, CancellationToken cancellationToken)
        {
            var insertedIds = await Mediator.Send(command, cancellationToken);

            return insertedIds == null ? NotFound() : Ok(new OperationStatus(insertedIds.Count, command.CategoryFileId));
        }

        [HttpPut("bulkdelete")]
        public async Task<IActionResult> BulkDelete(CategoryFileStoreBulkDeleteCommand command, CancellationToken cancellationToken)
        {
            var deleteCount = await Mediator.Send(command, cancellationToken);

            return !deleteCount.HasValue ? NotFound() : Ok(new OperationStatus(deleteCount.Value, command.CategoryFileId));
        }
    }
}
