using Engage.Application.Services.SupplierYears.Commands;
using Engage.Application.Services.SupplierYears.Queries;

namespace Engage.WebApi.Controllers;

public class SupplierYearController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<SupplierYearDto>>> DtoList([FromQuery] SupplierYearListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);
        return Ok(new ListResult<SupplierYearDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<SupplierYearOption>>> OptionList([FromQuery] SupplierYearOptionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);
        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SupplierYearVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new SupplierYearVmQuery { Id = id }, cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(SupplierYearInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.SupplierYearId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(SupplierYearUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.SupplierYearId));
    }

}
