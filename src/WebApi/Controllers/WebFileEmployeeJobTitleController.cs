// auto-generated
using Engage.Application.Services.WebFileEmployeeJobTitles.Commands;

namespace Engage.WebApi.Controllers;

public partial class WebFileEmployeeJobTitleController : BaseController
{
    [HttpPost("bulkinsert")]
    public async Task<IActionResult> BulkInsert(WebFileEmployeeJobTitleBulkInsertCommand command, CancellationToken cancellationToken)
    {
      var insertedIds = await Mediator.Send(command, cancellationToken);

      return insertedIds == null ? NotFound() : Ok(new OperationStatus(insertedIds.Count, command.WebFileId));
    }

    [HttpPut("bulkdelete")]
    public async Task<IActionResult> BulkDelete(WebFileEmployeeJobTitleBulkDeleteCommand command, CancellationToken cancellationToken)
    {
      var deleteCount = await Mediator.Send(command, cancellationToken);

      return !deleteCount.HasValue ?  NotFound() : Ok(new OperationStatus(deleteCount.Value, command.WebFileId));
    }


}