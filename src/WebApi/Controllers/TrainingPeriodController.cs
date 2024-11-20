using Engage.Application.Services.TrainingPeriods.Commands;
using Engage.Application.Services.TrainingPeriods.Models;
using Engage.Application.Services.TrainingPeriods.Queries;

namespace Engage.WebApi.Controllers;

public class TrainingPeriodController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<TrainingPeriodDto>>> DtoList([FromQuery] TrainingPeriodsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("options")]
    public async Task<ActionResult<List<OptionDto>>> OptionList([FromQuery] TrainingPeriodOptionsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TrainingPeriodVm>> GetVm([FromRoute] TrainingPeriodVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTrainingPeriodCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateTrainingPeriodCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
