using Engage.Application.Services.CategoryFileEmployees.Commands;
using Engage.Application.Services.CategoryFileEmployees.Queries;
using Engage.Application.Services.CategoryFiles.Queries;


namespace Engage.WebApi.Controllers
{
    public partial class CategoryFileEmployeeController : BaseController
    {
        [HttpGet()]
        public async Task<ActionResult<CategoryFileAdvancedTargetingVm>> Targeted([FromQuery] int employeeId, [FromQuery] int storeId, [FromQuery] DateTime date)
        {
            if (employeeId <= 0 || storeId <= 0)
            {
                return BadRequest(BadIdText);
            }

            var result = await Mediator.Send(new CategoryFileEmployeeTargetedQuery(employeeId, storeId, date));

            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost("bulkinsert")]
        public async Task<IActionResult> BulkInsert(CategoryFileEmployeeBulkInsertCommand command, CancellationToken cancellationToken)
        {
            var insertedIds = await Mediator.Send(command, cancellationToken);

            return insertedIds == null ? NotFound() : Ok(new OperationStatus(insertedIds.Count, command.CategoryFileId));
        }

        [HttpPut("bulkdelete")]
        public async Task<IActionResult> BulkDelete(CategoryFileEmployeeBulkDeleteCommand command, CancellationToken cancellationToken)
        {
            var deleteCount = await Mediator.Send(command, cancellationToken);

            return !deleteCount.HasValue ? NotFound() : Ok(new OperationStatus(deleteCount.Value, command.CategoryFileId));
        }
    }
}
