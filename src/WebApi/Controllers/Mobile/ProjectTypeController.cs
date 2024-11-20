using Engage.Application.Services.ProjectTypes.Queries;

namespace Engage.WebApi.Controllers.Mobile;

public partial class ProjectTypeController : BaseMobileController
{
    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<ProjectTypeOption>>> TyypeOptions([FromQuery] ProjectTypeOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }
}
