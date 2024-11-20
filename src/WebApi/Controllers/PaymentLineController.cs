using Engage.Application.Services.PaymentLines.Commands;
using Engage.Application.Services.PaymentLines.Queries;

namespace Engage.WebApi.Controllers;

public partial class PaymentLineController : BaseController
{
    [HttpGet("paymentid/{paymentId}")]
    public async Task<ActionResult<ListResult<PaymentLineDto>>> List([FromRoute] PaymentLineListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<PaymentLineDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<PaymentLineOption>>> Options([FromQuery] PaymentLineOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PaymentLineVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new PaymentLineVmQuery(id), cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(PaymentLineInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.PaymentLineId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(PaymentLineUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.PaymentLineId));
    }

    [HttpPost("amount")]
    public async Task<IActionResult> UpdateAmount(PaymentLineAmountUpdateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPost("note")]
    public async Task<IActionResult> UpdateNote(PaymentLineNoteUpdateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("quantity")]
    public async Task<IActionResult> UpdateQuantity(PaymentLineQuantityUpdateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(DeletePaymentLineCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }
}
