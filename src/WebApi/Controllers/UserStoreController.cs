using Engage.Application.Services.UserStores.Commands;
using Engage.Application.Services.UserStores.Queries;

namespace Engage.WebApi.Controllers;

public partial class UserStoreController : BaseController
{
    [HttpGet("userid/{userId}")]
    public async Task<ActionResult<ListResult<UserStoreDto>>> List([FromRoute] UserStoreListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<UserStoreDto>(entities));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserStoreVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new UserStoreVmQuery(id), cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(UserStoreInsertCommand command, CancellationToken cancellationToken)
    {
        var opStatus = await Mediator.Send(command, cancellationToken);

        return Ok(opStatus);
    }

    [HttpDelete("{id}")]
    public async override Task<IActionResult> Delete([FromRoute] int id)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new UserStoreDeleteCommand(id));

        return entity == null ? NotFound() : Ok(new OperationStatus(id));
    }

}
