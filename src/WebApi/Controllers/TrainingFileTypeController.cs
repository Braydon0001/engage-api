using Engage.Application.Services.TrainingFileTypes.Commands;
using Engage.Application.Services.TrainingFileTypes.Queries;

namespace Engage.WebApi.Controllers;

public partial class TrainingFileTypeController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<TrainingFileTypeDto>>> DtoList([FromQuery] TrainingFileTypeListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<TrainingFileTypeDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<TrainingFileTypeOption>>> OptionList([FromQuery] TrainingFileTypeOptionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TrainingFileTypeVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new TrainingFileTypeVmQuery { Id = id }, cancellationToken);
        if (entity == null)
        {
            return NotFound();
        }

        return entity;
    }

    [HttpPost]
    public async Task<IActionResult> Insert(TrainingFileTypeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.TrainingFileTypeId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(TrainingFileTypeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.TrainingFileTypeId));
    }
}