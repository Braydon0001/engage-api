using Engage.Application.Services.Locations.Commands;
using Engage.Application.Services.Locations.Models;
using Engage.Application.Services.Locations.Queries;

namespace Engage.WebApi.Controllers;

[Authorize("store")]
public class StoreLocationController : BaseController
{

    [HttpGet("storeId/{storeId}")]
    public async Task<ActionResult<ListResult<LocationListItemDto>>> GetLocations([FromRoute] int storeId)
    {
        return Ok(await Mediator.Send(new LocationsQuery
        {
            StakeholderType = StakeholderTypes.Store,
            EntityId = storeId,
        }));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ListResult<LocationVm>>> GetVm([FromRoute] LocationVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost()]
    public async Task<IActionResult> Create(CreateLocationCommand command)
    {
        command.StakeholderType = StakeholderTypes.Store;
        return Ok(await Mediator.Send(command));
    }

    [HttpPut()]
    public async Task<IActionResult> Update(UpdateLocationCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{id}")]
    public override async Task<IActionResult> Delete([FromRoute] int id)
    {
        return Ok(await Mediator.Send(new DeleteCommand
        {
            EntityName = "location",
            Id = id
        }));
    }
}
