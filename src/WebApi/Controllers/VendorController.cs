using Engage.Application.Services.Vendors.Commands;
using Engage.Application.Services.Vendors.Models;
using Engage.Application.Services.Vendors.Queries;

namespace Engage.WebApi.Controllers;

[Authorize("supplier")]
public class VendorController : BaseController
{
    [HttpGet("supplierId/{supplierId}")]
    public async Task<ActionResult<ListResult<VendorDto>>> GetBySupplier([FromRoute] VendorsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<VendorVm>> GetVm([FromRoute] GetVendorVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("options")]
    public async Task<ActionResult<List<VendorOptionDto>>> GetOptions([FromQuery] VendorOptionQuery query, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(query, cancellationToken));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateVendorCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateVendorCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
