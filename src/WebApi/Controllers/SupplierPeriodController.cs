using Engage.Application.Services.SupplierPeriods.Commands;
using Engage.Application.Services.SupplierPeriods.Queries;

namespace Engage.WebApi.Controllers;

public class SupplierPeriodController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<SupplierPeriodDto>>> DtoList([FromQuery] SupplierPeriodListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<SupplierPeriodDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<SupplierPeriodOption>>> OptionList([FromQuery] SupplierPeriodOptionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SupplierPeriodVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new SupplierPeriodVmQuery { Id = id }, cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(SupplierPeriodInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.SupplierPeriodId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(SupplierPeriodUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.SupplierPeriodId));
    }
}
