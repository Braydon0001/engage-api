using Engage.Application.Services.TrainingTypes.Commands;
using Engage.Application.Services.TrainingTypes.Models;
using Engage.Application.Services.TrainingTypes.Queries;

namespace Engage.WebApi.Controllers;

[Authorize("employee")]
public class TrainingTypeController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<TrainingTypeDto>>> GetAll([FromRoute] TrainingTypesQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TrainingTypeVm>> GetVm([FromRoute] TrainingTypeVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("option")]
    public async Task<ActionResult<ListResult<OptionDto>>> GetOptions([FromRoute] TrainingTypeOptionsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTrainingTypeCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateTrainingTypeCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
