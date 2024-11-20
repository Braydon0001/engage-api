using Engage.Application.Services.Projects.Queries;

namespace Engage.WebApi.Controllers;

public partial class ProjectController : BaseController
{
    [HttpPost("page")]
    public async Task<ActionResult<ListResult<ProjectDto>>> Paginated(ProjectPaginatedQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<ProjectDto>(entities));
    }

    [AllowAnonymous]
    [HttpGet("option")]
    public async Task<ActionResult<List<OptionDto>>> GetOptions([FromRoute] ProjectOptionsQuery query, [FromQuery] string search, bool isRegional)
    {
        query.Search = search;
        query.IsRegional = isRegional;
        return Ok(await Mediator.Send(query));
    }

}
