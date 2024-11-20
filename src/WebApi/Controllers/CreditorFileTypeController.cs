using Engage.Application.Services.CreditorFileTypes.Commands;
using Engage.Application.Services.CreditorFileTypes.Queries;

namespace Engage.WebApi.Controllers;

public partial class CreditorFileTypeController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<CreditorFileTypeDto>>> List([FromQuery] CreditorFileTypeListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<CreditorFileTypeDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<CreditorFileTypeOption>>> Options([FromQuery] CreditorFileTypeOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CreditorFileTypeVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new CreditorFileTypeVmQuery(id), cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(CreditorFileTypeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.CreditorFileTypeId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(CreditorFileTypeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.CreditorFileTypeId));
    }

}
