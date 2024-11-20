using Engage.Application.Services.Vat.Commands;
using Engage.Application.Services.Vat.Models;
using Engage.Application.Services.Vat.Queries;

namespace Engage.WebApi.Controllers;

public class VatController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<VatDto>>> GetAll([FromQuery] VatListQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<VatVm>> GetVm([FromRoute] VatVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<VatOption>>> Options([FromQuery] VatOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateVatCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateVatCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
