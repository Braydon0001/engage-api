using Engage.Application.Services.SurveyFormEmployees.Commands;
using Engage.Application.Services.SurveyFormEmployees.Queries;


namespace Engage.WebApi.Controllers
{
    public partial class SurveyFormEmployeeController : BaseController
    {
        [HttpGet("targeted/{surveyFormId}")]
        public async Task<IActionResult> BulkInsert([FromRoute] int surveyFormId, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new SurveyFormTargetedEmployeesQuery { Id = surveyFormId }, cancellationToken);

            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost("bulkinsert")]
        public async Task<IActionResult> BulkInsert(SurveyFormEmployeeBulkInsertCommand command, CancellationToken cancellationToken)
        {
            var insertedIds = await Mediator.Send(command, cancellationToken);

            return insertedIds == null ? NotFound() : Ok(new OperationStatus(insertedIds.Count, command.SurveyFormId));
        }

        [HttpPost("import")]
        public async Task<IActionResult> Import(SurveyFormEmployeeImportCommand command, CancellationToken cancellationToken)
        {
            var insertedIds = await Mediator.Send(command, cancellationToken);

            return insertedIds == null ? NotFound() : Ok(new OperationStatus(insertedIds.Count, command.SurveyFormId));
        }

        [HttpPut("bulkdelete")]
        public async Task<IActionResult> BulkDelete(SurveyFormEmployeeBulkDeleteCommand command, CancellationToken cancellationToken)
        {
            var deleteCount = await Mediator.Send(command, cancellationToken);

            return !deleteCount.HasValue ? NotFound() : Ok(new OperationStatus(deleteCount.Value, command.SurveyFormId));
        }
    }
}
