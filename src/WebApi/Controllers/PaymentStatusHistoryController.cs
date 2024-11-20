using Engage.Application.Services.PaymentStatusHistories.Commands;
using Engage.Application.Services.PaymentStatusHistories.Queries;

namespace Engage.WebApi.Controllers;

public partial class PaymentStatusHistoryController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<PaymentStatusHistoryDto>>> List([FromQuery] PaymentStatusHistoryListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<PaymentStatusHistoryDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<PaymentStatusHistoryOption>>> Options([FromQuery] PaymentStatusHistoryOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PaymentStatusHistoryVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new PaymentStatusHistoryVmQuery(id), cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(PaymentStatusHistoryInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(entity);
    }

    [HttpPut]
    public async Task<IActionResult> Update(PaymentStatusHistoryUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.PaymentStatusHistoryId));
    }

}
