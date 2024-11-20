using Engage.Application.Services.Employees.Queries;
using Engage.Application.Services.SurveyFormExcludedEmployees.Commands;
using Engage.Application.Services.SurveyFormExcludedEmployees.Queries;

namespace Engage.WebApi.Controllers
{
    public partial class SurveyFormExcludedEmployeeController : BaseController
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<List<EmployeeDto>>> Targets([FromRoute] int id, CancellationToken cancellationToken)
        {
            if (id <= 0)
            {
                return BadRequest(BadIdText);
            }

            var entity = await Mediator.Send(new SurveyFormExcludedEmployeeTargetsQuery { Id = id }, cancellationToken);

            return entity == null ? NotFound() : Ok(entity);
        }

        [HttpPost("bulkinsert")]
        public async Task<IActionResult> BulkInsert(SurveyFormExcludedEmployeeBulkInsertCommand command, CancellationToken cancellationToken)
        {
            var insertedIds = await Mediator.Send(command, cancellationToken);

            return insertedIds == null ? NotFound() : Ok(new OperationStatus(insertedIds.Count, command.SurveyFormId));
        }

        [HttpPut("bulkdelete")]
        public async Task<IActionResult> BulkDelete(SurveyFormExcludedEmployeeBulkDeleteCommand command, CancellationToken cancellationToken)
        {
            var deleteCount = await Mediator.Send(command, cancellationToken);

            return !deleteCount.HasValue ? NotFound() : Ok(new OperationStatus(deleteCount.Value, command.SurveyFormId));
        }
    }
}
