// auto-generated
using Engage.Application.Services.PayrollPeriods.Commands;
using Engage.Application.Services.PayrollPeriods.Queries;

namespace Engage.WebApi.Controllers;

public partial class PayrollPeriodController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<PayrollPeriodDto>>> DtoList([FromQuery] PayrollPeriodListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<PayrollPeriodDto>(entities));
    }

    [HttpGet("option")]
    public async Task<ActionResult<IEnumerable<PayrollPeriodOption>>> OptionList([FromQuery] PayrollPeriodOptionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PayrollPeriodVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new PayrollPeriodVmQuery { Id = id }, cancellationToken);
        if (entity == null)
        {
            return NotFound();
        }

        return entity;
    }

    [HttpPost]
    public async Task<IActionResult> Insert(PayrollPeriodInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.PayrollPeriodId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(PayrollPeriodUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.PayrollPeriodId));
    }
}