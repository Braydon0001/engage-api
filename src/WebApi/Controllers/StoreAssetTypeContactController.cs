using Engage.Application.Services.StoreAssetTypeContacts.Commands;
using Engage.Application.Services.StoreAssetTypeContacts.Queries;

namespace Engage.WebApi.Controllers;

public partial class StoreAssetTypeContactController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<StoreAssetTypeContactDto>>> List([FromQuery] StoreAssetTypeContactListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<StoreAssetTypeContactDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<StoreAssetTypeContactOption>>> Options([FromQuery] StoreAssetTypeContactOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<StoreAssetTypeContactVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new StoreAssetTypeContactVmQuery(id), cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(StoreAssetTypeContactInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.StoreAssetTypeContactId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(StoreAssetTypeContactUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.StoreAssetTypeContactId));
    }

}
