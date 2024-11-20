using Engage.Application.Services.TrainingDurations.Commands;
using Engage.Application.Services.TrainingDurations.Queries;

namespace Engage.WebApi.Controllers;

public partial class TrainingDurationController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<TrainingDurationDto>>> List([FromQuery] TrainingDurationListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<TrainingDurationDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<TrainingDurationOption>>> Options([FromQuery] TrainingDurationOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TrainingDurationVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new TrainingDurationVmQuery(id), cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(TrainingDurationInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.TrainingDurationId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(TrainingDurationUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.TrainingDurationId));
    }
}
