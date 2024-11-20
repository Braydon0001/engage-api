using Engage.Application.Services.CategoryFileTargets.Commands;
using Engage.Application.Services.CategoryFileTargets.Queries;
using Engage.Domain.Entities.Json;

namespace Engage.WebApi.Controllers
{
    public partial class CategoryFileTargetController : BaseController
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryFileTargets>> Targets([FromRoute] int id, CancellationToken cancellationToken)
        {
            if (id <= 0)
            {
                return BadRequest(BadIdText);
            }

            var entity = await Mediator.Send(new CategoryFileTargetsQuery { Id = id }, cancellationToken);

            return entity == null ? NotFound() : Ok(entity);
        }

        [HttpGet("options/{id}")]
        public async Task<ActionResult<CategoryFileTargetOptions>> TargetOptions([FromRoute] int id, CancellationToken cancellationToken)
        {
            if (id <= 0)
            {
                return BadRequest(BadIdText);
            }

            var entity = await Mediator.Send(new CategoryFileTargetOptionsQuery { Id = id }, cancellationToken);

            return entity == null ? NotFound() : Ok(entity);
        }

        [HttpGet("model/{id}")]
        public async Task<ActionResult<JsonRule>> GetTargetModel([FromRoute] int id, CancellationToken cancellationToken)
        {
            if (id <= 0)
            {
                return BadRequest(BadIdText);
            }

            var entity = await Mediator.Send(new CategoryFileTargetModelQuery { Id = id }, cancellationToken);

            return entity == null ? NotFound() : Ok(entity);
        }


        [HttpPost]
        public async Task<IActionResult> SetTargetModel(CategoryFileTargetModelSetCommand command, CancellationToken cancellationToken)
        {

            return Ok(await Mediator.Send(command, cancellationToken));
        }
    }
}
