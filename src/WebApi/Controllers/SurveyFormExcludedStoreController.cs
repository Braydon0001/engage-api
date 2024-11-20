using Engage.Application.Services.Stores.Queries;
using Engage.Application.Services.SurveyFormExcludedStores.Commands;
using Engage.Application.Services.SurveyFormExcludedStores.Queries;

namespace Engage.WebApi.Controllers
{
    public partial class SurveyFormExcludedStoreController : BaseController
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<List<StoreDto>>> Targets([FromRoute] int id, CancellationToken cancellationToken)
        {
            if (id <= 0)
            {
                return BadRequest(BadIdText);
            }

            var entity = await Mediator.Send(new SurveyFormExcludedStoreTargetsQuery { Id = id }, cancellationToken);

            return entity == null ? NotFound() : Ok(entity);
        }

        [HttpPost("bulkinsert")]
        public async Task<IActionResult> BulkInsert(SurveyFormExcludedStoreBulkInsertCommand command, CancellationToken cancellationToken)
        {
            var insertedIds = await Mediator.Send(command, cancellationToken);

            return insertedIds == null ? NotFound() : Ok(new OperationStatus(insertedIds.Count, command.SurveyFormId));
        }

        [HttpPut("bulkdelete")]
        public async Task<IActionResult> BulkDelete(SurveyFormExcludedStoreBulkDeleteCommand command, CancellationToken cancellationToken)
        {
            var deleteCount = await Mediator.Send(command, cancellationToken);

            return !deleteCount.HasValue ? NotFound() : Ok(new OperationStatus(deleteCount.Value, command.SurveyFormId));
        }
    }
}
