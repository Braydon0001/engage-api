using Engage.Application.Services.ProjectCategories.Queries;

namespace Engage.WebApi.Controllers.Mobile;

public partial class ProjectCategoryController : BaseMobileController
{
    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<ProjectCategoryOption>>> CategoryOptions([FromQuery] ProjectCategoryOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }
}
