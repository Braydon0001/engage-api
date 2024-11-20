using Engage.Application.Services.SurveyTargets.Queries;

namespace Engage.WebApi.Controllers;

public partial class SurveyTargetController : BaseController
{
    [HttpGet("{id}")]
    public async Task<ActionResult<SurveyTargets>> Targets([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new SurveyTargetsQuery { Id = id }, cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }
}