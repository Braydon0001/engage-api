// auto-generated
using Engage.Application.Services.SupplierBudgetTypes.Commands;
using Engage.Application.Services.SupplierBudgetTypes.Queries;

namespace Engage.WebApi.Controllers;

public partial class SupplierBudgetTypeController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<SupplierBudgetTypeDto>>> DtoList([FromQuery]SupplierBudgetTypeListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<SupplierBudgetTypeDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<SupplierBudgetTypeOption>>> OptionList([FromQuery]SupplierBudgetTypeOptionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SupplierBudgetTypeVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new SupplierBudgetTypeVmQuery { Id = id }, cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(SupplierBudgetTypeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.SupplierBudgetTypeId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(SupplierBudgetTypeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.SupplierBudgetTypeId));
    }


}