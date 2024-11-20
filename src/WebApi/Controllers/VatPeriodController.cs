using Engage.Application.Services.VatPeriods.Queries;
using Engage.Application.Services.VatPeriods.Commands;
using Engage.Application.Services.VatPeriods.Models;

namespace Engage.WebApi.Controllers;

public class VatPeriodController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<VatPeriodDto>>> GetAll([FromQuery] VatPeriodsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<VatPeriodVm>> GetVm([FromRoute] VatPeriodVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateVatPeriodCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateVatPeriodCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
