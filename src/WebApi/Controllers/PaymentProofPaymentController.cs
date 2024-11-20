using Engage.Application.Services.PaymentProofPayments.Commands;
using Engage.Application.Services.PaymentProofPayments.Queries;

namespace Engage.WebApi.Controllers;

public partial class PaymentProofPaymentController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<PaymentProofPaymentDto>>> List([FromQuery] PaymentProofPaymentListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<PaymentProofPaymentDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<PaymentProofPaymentOption>>> Options([FromQuery] PaymentProofPaymentOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PaymentProofPaymentVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new PaymentProofPaymentVmQuery(id), cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(PaymentProofPaymentInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.PaymentProofPaymentId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(PaymentProofPaymentUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.PaymentProofPaymentId));
    }

}
