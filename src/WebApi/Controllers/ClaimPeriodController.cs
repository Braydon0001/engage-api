using Engage.Application.Services.ClaimPeriods.Commands;
using Engage.Application.Services.ClaimPeriods.Models;
using Engage.Application.Services.ClaimPeriods.Queries;

namespace Engage.WebApi.Controllers;

public class ClaimPeriodController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<ClaimPeriodDto>>> DtoList([FromQuery] ClaimPeriodsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("options")]
    public async Task<ActionResult<List<OptionDto>>> OptionList([FromQuery] ClaimPeriodOptionsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<ClaimPeriodVm>> GetVm([FromRoute] ClaimPeriodVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateClaimPeriodCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateClaimPeriodCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
