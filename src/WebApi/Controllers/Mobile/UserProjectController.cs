using Engage.Application.Services.Projects.Queries;
using Engage.Application.Services.ProjectStores.Queries;


namespace Engage.WebApi.Controllers.Mobile;

public partial class UserProjectController : BaseMobileController
{

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<ListResult<ProjectDto>>> ProjectsByUserId([FromRoute] int userId, [FromQuery] string search, CancellationToken cancellationToken)
    {
        if (userId <= 0)
        {
            return BadRequest(BadIdText);
        }
        var entities = await Mediator.Send(new ProjectStorePaginatedQuery { UserId = userId, Search = search }, cancellationToken);

        return Ok(new ListResult<ProjectDto>(entities));
    }
}
