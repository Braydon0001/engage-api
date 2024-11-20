using Engage.Application.Services.TrainingProviders.Commands;
using Engage.Application.Services.TrainingProviders.Models;
using Engage.Application.Services.TrainingProviders.Queries;

namespace Engage.WebApi.Controllers;

[Authorize("employee")]
public class TrainingProviderController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<TrainingProviderDto>>> GetAll([FromRoute] TrainingProvidersQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TrainingProviderVm>> GetVm([FromRoute] TrainingProviderVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("option")]
    public async Task<ActionResult<ListResult<OptionDto>>> GetOptions([FromRoute] TrainingProviderOptionsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTrainingProviderCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateTrainingProviderCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
