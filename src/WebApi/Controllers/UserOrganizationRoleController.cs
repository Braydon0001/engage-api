using Engage.Application.Services.UserOrganizationRoles.Commands;
using Engage.Application.Services.UserOrganizationRoles.Queries;

namespace Engage.WebApi.Controllers;

public partial class UserOrganizationRoleController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<UserOrganizationRoleDto>>> List([FromQuery] UserOrganizationRoleListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<UserOrganizationRoleDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<UserOrganizationRoleOption>>> Options([FromQuery] UserOrganizationRoleOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserOrganizationRoleVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new UserOrganizationRoleVmQuery(id), cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(UserOrganizationRoleInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.UserOrganizationRoleId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UserOrganizationRoleUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.UserOrganizationRoleId));
    }

}
