using Engage.Application.Services.CommunicationTypes.Commands;
using Engage.Application.Services.CommunicationTypes.Queries;

namespace Engage.WebApi.Controllers;

public partial class CommunicationTypeController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<CommunicationTypeDto>>> List([FromQuery] CommunicationTypeListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<CommunicationTypeDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<CommunicationTypeOption>>> Options([FromQuery] CommunicationTypeOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CommunicationTypeVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new CommunicationTypeVmQuery(id), cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(CommunicationTypeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.CommunicationTypeId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(CommunicationTypeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.CommunicationTypeId));
    }

}
