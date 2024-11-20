using Engage.Application.Services.DistributionCenters.Commands;
using Engage.Application.Services.DistributionCenters.Models;
using Engage.Application.Services.DistributionCenters.Queries;

namespace Engage.WebApi.Controllers;

[Authorize("dc")]
public class DistributionCenterController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<DistributionCenterDto>>> DtoList([FromRoute] DistributionCentersQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [AllowAnonymous]
    [HttpGet("[action]/{storeid}")]
    public async Task<ActionResult<List<CascadingOptionDto>>> GetDistributionCentersByStore([FromRoute] DistributionCentersByStoreQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DistributionCenterVm>> Vm([FromRoute] DistributionCenterVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Insert(CreateDistributionCenterCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateDistributionCenterCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
