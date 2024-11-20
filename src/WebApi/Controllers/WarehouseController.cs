using Engage.Application.Services.Warehouses.Commands;
using Engage.Application.Services.Warehouses.Models;
using Engage.Application.Services.Warehouses.Queries;

namespace Engage.WebApi.Controllers;

[Authorize("dc")]
public class WarehouseController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<WarehouseDto>>> GetAll([FromRoute] WarehousesQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<WarehouseVm>> GetVM([FromRoute] GetWarehouseViewModelQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("options/distributionCenterId/{distributionCenterId}")]
    public async Task<ActionResult<WarehouseVm>> GetVM([FromRoute] WarehouseOptionsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateWarehouseCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Put(UpdateWarehouseCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
