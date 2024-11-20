using Engage.Application.Services.PaymentYears.Commands;
using Engage.Application.Services.PaymentYears.Queries;

namespace Engage.WebApi.Controllers;

public partial class PaymentYearController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<PaymentYearDto>>> List([FromQuery] PaymentYearListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<PaymentYearDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<PaymentYearOption>>> Options([FromQuery] PaymentYearOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PaymentYearVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new PaymentYearVmQuery(id), cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(PaymentYearInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.PaymentYearId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(PaymentYearUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.PaymentYearId));
    }

}
