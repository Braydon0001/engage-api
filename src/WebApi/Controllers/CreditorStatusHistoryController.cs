using Engage.Application.Services.CreditorStatusHistories.Commands;
using Engage.Application.Services.CreditorStatusHistories.Queries;

namespace Engage.WebApi.Controllers;

public partial class CreditorStatusHistoryController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<CreditorStatusHistoryDto>>> List([FromQuery] CreditorStatusHistoryListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<CreditorStatusHistoryDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<CreditorStatusHistoryOption>>> Options([FromQuery] CreditorStatusHistoryOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CreditorStatusHistoryVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new CreditorStatusHistoryVmQuery(id), cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(CreditorStatusHistoryInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(entity);
    }

    [HttpPut]
    public async Task<IActionResult> Update(CreditorStatusHistoryUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.CreditorStatusHistoryId));
    }

}
