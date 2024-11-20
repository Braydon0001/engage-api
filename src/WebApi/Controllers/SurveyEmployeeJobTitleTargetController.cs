// auto-generated

using Engage.Application.Services.SurveyEmployeeJobTitles.Commands;

namespace Engage.WebApi.Controllers;

public partial class SurveyEmployeeJobTitleTargetController : BaseController
{
    [HttpPost("bulkinsert")]
    public async Task<IActionResult> BulkInsert(SurveyEmployeeJobTitleBulkInsertCommand command, CancellationToken cancellationToken)
    {
        var insertedIds = await Mediator.Send(command, cancellationToken);

        return insertedIds == null ? NotFound() : Ok(new OperationStatus(insertedIds.Count, command.SurveyId));
    }

    [HttpPut("bulkdelete")]
    public async Task<IActionResult> BulkDelete(SurveyEmployeeJobTitleBulkDeleteCommand command, CancellationToken cancellationToken)
    {
        var deleteCount = await Mediator.Send(command, cancellationToken);

        return !deleteCount.HasValue ? NotFound() : Ok(new OperationStatus(deleteCount.Value, command.SurveyId));
    }
}