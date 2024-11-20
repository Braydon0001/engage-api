using Engage.Application.Services.ProjectSubTypes.Queries;

namespace Engage.WebApi.Controllers.Mobile;

public partial class ProjectSubTypeController : BaseMobileController
{
    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<ProjectSubTypeOption>>> SubTypeOptions([FromQuery] ProjectSubTypeOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }
}
