using Engage.Application.Services.ProjectUsers.Commands;
using Engage.Application.Services.ProjectUsers.Queries;

namespace Engage.WebApi.Controllers;

public partial class ProjectUserController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<ProjectUserDto>>> List([FromQuery] ProjectUserListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<ProjectUserDto>(entities));
    }

    [HttpPost]
    public async Task<IActionResult> Insert(ProjectUserInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.UserId));
    }

}
