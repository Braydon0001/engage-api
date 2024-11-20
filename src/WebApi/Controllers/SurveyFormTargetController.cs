using Engage.Application.Services.SurveyFormTargets.Commands;
using Engage.Application.Services.SurveyFormTargets.Queries;
using Engage.Domain.Entities.Json;

namespace Engage.WebApi.Controllers
{
    public partial class SurveyFormTargetController : BaseController
    {
        [HttpGet()]
        public async Task<ActionResult<SurveyFormAdvancedTargetingDto>> Targeted([FromQuery] int employeeId, [FromQuery] int? storeId, [FromQuery] DateTime date)
        {
            if (employeeId <= 0)
            {
                return BadRequest(BadIdText);
            }

            var result = await Mediator.Send(new SurveyFormTargetedParamsQuery(employeeId, storeId, date));

            return result == null ? NotFound() : Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SurveyFormTargets>> Targets([FromRoute] int id, CancellationToken cancellationToken)
        {
            if (id <= 0)
            {
                return BadRequest(BadIdText);
            }

            var entity = await Mediator.Send(new SurveyFormTargetsQuery { Id = id }, cancellationToken);

            return entity == null ? NotFound() : Ok(entity);
        }

        [HttpGet("options/{id}")]
        public async Task<ActionResult<SurveyFormTargetOptions>> TargetOptions([FromRoute] int id, CancellationToken cancellationToken)
        {
            if (id <= 0)
            {
                return BadRequest(BadIdText);
            }

            var entity = await Mediator.Send(new SurveyFormTargetOptionsQuery { Id = id }, cancellationToken);

            return entity == null ? NotFound() : Ok(entity);
        }

        [HttpGet("model/{id}")]
        public async Task<ActionResult<JsonRule>> GetTargetModel([FromRoute] int id, CancellationToken cancellationToken)
        {
            if (id <= 0)
            {
                return BadRequest(BadIdText);
            }

            var entity = await Mediator.Send(new SurveyFormTargetModelQuery { Id = id }, cancellationToken);

            return entity == null ? NotFound() : Ok(entity);
        }


        [HttpPost]
        public async Task<IActionResult> SetTargetModel(SurveyFormTargetModelSetCommand command, CancellationToken cancellationToken)
        {

            return Ok(await Mediator.Send(command, cancellationToken));
        }
    }
}
