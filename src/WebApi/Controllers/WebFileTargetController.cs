using Engage.Application.Services.WebFileTargets.Queries;

namespace Engage.WebApi.Controllers;

public partial class WebFileTargetController : BaseController
{
    [HttpGet("{id}")]
    public async Task<ActionResult<WebFileTargets>> Targets([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new WebFileTargetsQuery { Id = id }, cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }
}