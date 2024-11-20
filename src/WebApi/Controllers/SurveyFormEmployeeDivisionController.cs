using Engage.Application.Services.SurveyFormEmployeeDivisions.Commands;


namespace Engage.WebApi.Controllers
{
    public partial class SurveyFormEmployeeDivisionController : BaseController
    {
        [HttpPost("bulkinsert")]
        public async Task<IActionResult> BulkInsert(SurveyFormEmployeeDivisionBulkInsertCommand command, CancellationToken cancellationToken)
        {
            var insertedIds = await Mediator.Send(command, cancellationToken);

            return insertedIds == null ? NotFound() : Ok(new OperationStatus(insertedIds.Count, command.SurveyFormId));
        }

        [HttpPut("bulkdelete")]
        public async Task<IActionResult> BulkDelete(SurveyFormEmployeeDivisionBulkDeleteCommand command, CancellationToken cancellationToken)
        {
            var deleteCount = await Mediator.Send(command, cancellationToken);

            return !deleteCount.HasValue ? NotFound() : Ok(new OperationStatus(deleteCount.Value, command.SurveyFormId));
        }
    }
}
