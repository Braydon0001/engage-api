// auto-generated
using Engage.Application.Services.CategoryFileEmployeeJobTitles.Commands;

namespace Engage.WebApi.Controllers;

public partial class CategoryFileEmployeeJobTitleController : BaseController
{
    [HttpPost("bulkinsert")]
    public async Task<IActionResult> BulkInsert(CategoryFileEmployeeJobTitleBulkInsertCommand command, CancellationToken cancellationToken)
    {
        var insertedIds = await Mediator.Send(command, cancellationToken);

        return insertedIds == null ? NotFound() : Ok(new OperationStatus(insertedIds.Count, command.CategoryFileId));
    }

    [HttpPut("bulkdelete")]
    public async Task<IActionResult> BulkDelete(CategoryFileEmployeeJobTitleBulkDeleteCommand command, CancellationToken cancellationToken)
    {
        var deleteCount = await Mediator.Send(command, cancellationToken);

        return !deleteCount.HasValue ? NotFound() : Ok(new OperationStatus(deleteCount.Value, command.CategoryFileId));
    }


}