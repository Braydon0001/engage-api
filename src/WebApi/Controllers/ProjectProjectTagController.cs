using Engage.Application.Services.ProjectProjectTags.Commands;
using Engage.Application.Services.ProjectProjectTags.Queries;

namespace Engage.WebApi.Controllers;

public partial class ProjectProjectTagController : BaseController
{
    [HttpGet("projectid/{projectId}")]
    public async Task<ActionResult<ListResult<ProjectProjectTagDto>>> List([FromRoute] ProjectProjectTagListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<ProjectProjectTagDto>(entities));
    }

    [HttpPost]
    public async Task<IActionResult> Insert(ProjectProjectTagInsertCommand command, CancellationToken cancellationToken)
    {
        var opStatus = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(opStatus.Status));
    }

    [HttpDelete("{id}")]
    public async override Task<IActionResult> Delete([FromRoute] int id)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new ProjectProjectTagDeleteCommand(id));

        return entity == null ? NotFound() : Ok(new OperationStatus(id));
    }
}
