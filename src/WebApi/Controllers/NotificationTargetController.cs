using Engage.Application.Services.NotificationTargets.Queries;

namespace Engage.WebApi.Controllers;

public partial class NotificationTargetController : BaseController
{
    [HttpGet("{id}")]
    public async Task<ActionResult<NotificationTargets>> Targets([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new NotificationTargetsQuery { Id = id }, cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }
}