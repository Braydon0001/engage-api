using Engage.Application.Services.PaymentPeriods.Commands;
using Engage.Application.Services.PaymentPeriods.Queries;

namespace Engage.WebApi.Controllers;

public partial class PaymentPeriodController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<PaymentPeriodDto>>> List([FromQuery] PaymentPeriodListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<PaymentPeriodDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<PaymentPeriodOption>>> Options([FromQuery] PaymentPeriodOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PaymentPeriodVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new PaymentPeriodVmQuery(id), cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(PaymentPeriodInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.PaymentPeriodId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(PaymentPeriodUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.PaymentPeriodId));
    }

}
