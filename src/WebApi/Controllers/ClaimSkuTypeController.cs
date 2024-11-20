using Engage.Application.Services.ClaimSkuTypes.Commands;
using Engage.Application.Services.ClaimSkuTypes.Models;
using Engage.Application.Services.ClaimSkuTypes.Queries;

namespace Engage.WebApi.Controllers;

public class ClaimSkuTypeController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<PaginatedListResult<ClaimSkuTypeDto>>> GetAll([FromQuery] ClaimSkuTypesQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ClaimSkuTypeVm>> GetVm([FromRoute] ClaimSkuTypeVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateClaimSkuTypeCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateClaimSkuTypeCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
