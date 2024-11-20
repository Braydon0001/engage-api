using Engage.Application.Services.ProjectPriorities.Queries;

namespace Engage.WebApi.Controllers.Mobile;

public partial class ProjectPriorityController : BaseMobileController
{
    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<ProjectPriorityOption>>> PriorityOptions([FromQuery] ProjectPriorityOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }
}
