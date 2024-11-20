using Engage.Application.Services.SurveyFormStores.Commands;
using Engage.Application.Services.SurveyFormStores.Queries;


namespace Engage.WebApi.Controllers
{
    public partial class SurveyFormStoreController : BaseController
    {
        [HttpGet("targeted/{surveyFormId}")]
        public async Task<IActionResult> BulkInsert([FromRoute] int surveyFormId, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new SurveyFormTargetedStoresQuery { Id = surveyFormId }, cancellationToken);

            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost("bulkinsert")]
        public async Task<IActionResult> BulkInsert(SurveyFormStoreBulkInsertCommand command, CancellationToken cancellationToken)
        {
            var insertedIds = await Mediator.Send(command, cancellationToken);

            return insertedIds == null ? NotFound() : Ok(new OperationStatus(insertedIds.Count, command.SurveyFormId));
        }

        [HttpPost("import")]
        public async Task<IActionResult> Import(SurveyFormStoreImportCommand command, CancellationToken cancellationToken)
        {
            var insertedIds = await Mediator.Send(command, cancellationToken);

            return insertedIds == null ? NotFound() : Ok(new OperationStatus(insertedIds.Count, command.SurveyFormId));
        }

        [HttpPut("bulkdelete")]
        public async Task<IActionResult> BulkDelete(SurveyFormStoreBulkDeleteCommand command, CancellationToken cancellationToken)
        {
            var deleteCount = await Mediator.Send(command, cancellationToken);

            return !deleteCount.HasValue ? NotFound() : Ok(new OperationStatus(deleteCount.Value, command.SurveyFormId));
        }
    }
}
