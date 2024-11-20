// auto-generated
using Engage.Application.Services.PayrollYears.Commands;
using Engage.Application.Services.PayrollYears.Queries;

namespace Engage.WebApi.Controllers;

public partial class PayrollYearController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<PayrollYearDto>>> DtoList([FromQuery]PayrollYearListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<PayrollYearDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<PayrollYearOption>>> OptionList([FromQuery]PayrollYearOptionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PayrollYearVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new PayrollYearVmQuery { Id = id }, cancellationToken);
        if (entity == null)
        {
            return NotFound();
        }

        return entity;
    }

    [HttpPost]
    public async Task<IActionResult> Insert(PayrollYearInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.PayrollYearId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(PayrollYearUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.PayrollYearId));
    }
}