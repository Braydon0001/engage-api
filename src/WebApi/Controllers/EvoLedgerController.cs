using Engage.Application.Services.EvoLedgers.Commands;
using Engage.Application.Services.EvoLedgers.Queries;

namespace Engage.WebApi.Controllers;

public partial class EvoLedgerController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<EvoLedgerDto>>> List([FromQuery] EvoLedgerListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<EvoLedgerDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<EvoLedgerOption>>> Options([FromQuery] EvoLedgerOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EvoLedgerVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new EvoLedgerVmQuery(id), cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(EvoLedgerInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.EvoLedgerId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(EvoLedgerUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.EvoLedgerId));
    }

}
