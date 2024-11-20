using Engage.Application.Services.SupplierRegions.Commands;
using Engage.Application.Services.SupplierRegions.Models;
using Engage.Application.Services.SupplierRegions.Queries;

namespace Engage.WebApi.Controllers;

public class SupplierRegionController : BaseController
{
    [HttpGet("supplierId/{supplierId}")]
    public async Task<ActionResult<PaginatedListResult<SupplierRegionDto>>> GetAll([FromRoute] SupplierRegionsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("option/supplierId/{supplierId}")]
    public async Task<ActionResult<PaginatedListResult<OptionDto>>> GetOptions([FromRoute] SupplierRegionOptionsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<SupplierRegionVm>> GetVm([FromRoute] SupplierRegionVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateSupplierRegionCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateSupplierRegionCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
