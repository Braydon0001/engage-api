using Engage.Application.Services.ExternalUserTypes.Commands;
using Engage.Application.Services.ExternalUserTypes.Queries;

namespace Engage.WebApi.Controllers;

public partial class ExternalUserTypeController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<ExternalUserTypeDto>>> List([FromQuery] ExternalUserTypeListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<ExternalUserTypeDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<ExternalUserTypeOption>>> Options([FromQuery] ExternalUserTypeOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ExternalUserTypeVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new ExternalUserTypeVmQuery(id), cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(ExternalUserTypeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.ExternalUserTypeId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(ExternalUserTypeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.ExternalUserTypeId));
    }

}
