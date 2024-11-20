using Engage.Application.Services.SurveyFormStoreClusters.Commands;


namespace Engage.WebApi.Controllers
{
    public partial class SurveyFormStoreClusterController : BaseController
    {
        [HttpPost("bulkinsert")]
        public async Task<IActionResult> BulkInsert(SurveyFormStoreClusterBulkInsertCommand command, CancellationToken cancellationToken)
        {
            var insertedIds = await Mediator.Send(command, cancellationToken);

            return insertedIds == null ? NotFound() : Ok(new OperationStatus(insertedIds.Count, command.SurveyFormId));
        }

        [HttpPut("bulkdelete")]
        public async Task<IActionResult> BulkDelete(SurveyFormStoreClusterBulkDeleteCommand command, CancellationToken cancellationToken)
        {
            var deleteCount = await Mediator.Send(command, cancellationToken);

            return !deleteCount.HasValue ? NotFound() : Ok(new OperationStatus(deleteCount.Value, command.SurveyFormId));
        }
    }
}
