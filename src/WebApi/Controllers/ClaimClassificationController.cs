using Engage.Application.Services.ClaimClassifications.Commands;
using Engage.Application.Services.ClaimClassifications.Models;
using Engage.Application.Services.ClaimClassifications.Queries;

namespace Engage.WebApi.Controllers;

public class ClaimClassificationController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<PaginatedListResult<ClaimClassificationDto>>> GetAll([FromQuery] ClaimClassificationsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("option")]
    public async Task<ActionResult<List<ClaimClassificationVm>>> GetOptions([FromRoute] ClaimClassificationOptionsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("option/supplier")]
    public async Task<ActionResult<List<ClaimClassificationVm>>> GetOptionsByHostSupplier([FromRoute] ClaimClassificationOptionsQuery query)
    {
        query.IsSupplier = true;
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ClaimClassificationVm>> GetVm([FromRoute] ClaimClassificationVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateClaimClassificationCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateClaimClassificationCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
