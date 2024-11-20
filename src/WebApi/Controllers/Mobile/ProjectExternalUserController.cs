using Engage.Application.Services.ProjectExternalUsers.Commands;
using Engage.Application.Services.ProjectExternalUsers.Queries;

namespace Engage.WebApi.Controllers.Mobile;

public partial class ProjectExternalUserController : BaseMobileController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<ProjectExternalUserDto>>> List([FromQuery] ProjectExternalUserListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<ProjectExternalUserDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<ProjectExternalUserOption>>> Options([FromQuery] ProjectExternalUserOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectExternalUserVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new ProjectExternalUserVmQuery(id), cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(ProjectExternalUserInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(entity);
    }

    [HttpPut]
    public async Task<IActionResult> Update(ProjectExternalUserUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.ProjectExternalUserId));
    }

}
