using Engage.Application.Services.TargetStrategies.Queries;

namespace Engage.WebApi.Controllers;

public class TargetStrategyController : BaseController
{
    [HttpGet("options")]
    public async Task<ActionResult<List<TargetStrategyOption>>> OptionList([FromRoute] TargetStrategyOptionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }
}
