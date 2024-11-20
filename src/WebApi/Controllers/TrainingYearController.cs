using Engage.Application.Services.TrainingYears.Commands;
using Engage.Application.Services.TrainingYears.Models;
using Engage.Application.Services.TrainingYears.Queries;

namespace Engage.WebApi.Controllers;

public class TrainingYearController : BaseController
{

    [HttpGet]
    public async Task<ActionResult<ListResult<TrainingYearDto>>> GetAll([FromRoute] TrainingYearsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TrainingYearVm>> GetVm([FromRoute] TrainingYearVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTrainingYearCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateTrainingYearCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

}
