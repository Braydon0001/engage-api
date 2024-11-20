using Engage.Application.Services.ClaimFloats.Commands;
using Engage.Application.Services.ClaimFloats.Models;
using Engage.Application.Services.ClaimFloats.Queries;

namespace Engage.WebApi.Controllers;

public class ClaimFloatController : BaseController
{
    [HttpPost("page")]
    public async Task<ActionResult<ListResult<ClaimFloatDto>>> PaginatedQuery(ClaimFloatPaginatedQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ClaimFloatVm>> GetVm([FromRoute] ClaimFloatVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("options")]
    public async Task<ActionResult<List<OptionDto>>> GetOptionsByEmployee([FromQuery] ClaimFloatOptionsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateClaimFloatCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateClaimFloatCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("topUp")]
    public async Task<IActionResult> UpdateTopUp(UpdateClaimFloatTopUpCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
