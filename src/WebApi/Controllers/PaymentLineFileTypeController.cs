using Engage.Application.Services.PaymentLineFileTypes.Commands;
using Engage.Application.Services.PaymentLineFileTypes.Queries;

namespace Engage.WebApi.Controllers;

public partial class PaymentLineFileTypeController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<PaymentLineFileTypeDto>>> List([FromQuery] PaymentLineFileTypeListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<PaymentLineFileTypeDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<PaymentLineFileTypeOption>>> Options([FromQuery] PaymentLineFileTypeOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PaymentLineFileTypeVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new PaymentLineFileTypeVmQuery(id), cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(PaymentLineFileTypeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.PaymentLineFileTypeId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(PaymentLineFileTypeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.PaymentLineFileTypeId));
    }

}
