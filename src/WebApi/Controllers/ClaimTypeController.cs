using Engage.Application.Services.ClaimTypes.Commands;
using Engage.Application.Services.ClaimTypes.Models;
using Engage.Application.Services.ClaimTypes.Queries;

namespace Engage.WebApi.Controllers;

public class ClaimTypeController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<PaginatedListResult<ClaimTypeDto>>> GetAll([FromQuery] ClaimTypesQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("option")]
    public async Task<ActionResult<ClaimTypeVm>> GetOptions([FromQuery] ClaimTypeOptionsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("option/claimclassificationid/{claimclassificationid}")]
    public async Task<ActionResult<ClaimTypeVm>> GetOptionsByClaimClassificationId([FromRoute] int? claimclassificationid)
    {
        return Ok(await Mediator.Send(new ClaimTypeOptionsQuery
        {
            ClaimClassificationId = claimclassificationid,
        }));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ClaimTypeVm>> GetVm([FromRoute] ClaimTypeVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateClaimTypeCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateClaimTypeCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
