using Engage.Application.Services.ClaimYears.Commands;
using Engage.Application.Services.ClaimYears.Models;
using Engage.Application.Services.ClaimYears.Queries;

namespace Engage.WebApi.Controllers;

public class ClaimYearController : BaseController
{

    [HttpGet]
    public async Task<ActionResult<ListResult<ClaimYearDto>>> GetAll([FromRoute] ClaimYearsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ClaimYearVm>> GetVm([FromRoute] ClaimYearVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateClaimYearCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateClaimYearCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

}
