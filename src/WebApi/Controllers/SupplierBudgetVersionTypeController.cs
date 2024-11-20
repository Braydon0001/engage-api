// auto-generated
using Engage.Application.Services.SupplierBudgetVersionTypes.Commands;
using Engage.Application.Services.SupplierBudgetVersionTypes.Queries;

namespace Engage.WebApi.Controllers;

public partial class SupplierBudgetVersionTypeController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<SupplierBudgetVersionTypeDto>>> DtoList([FromQuery]SupplierBudgetVersionTypeListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<SupplierBudgetVersionTypeDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<SupplierBudgetVersionTypeOption>>> OptionList([FromQuery]SupplierBudgetVersionTypeOptionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SupplierBudgetVersionTypeVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new SupplierBudgetVersionTypeVmQuery { Id = id }, cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(SupplierBudgetVersionTypeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.SupplierBudgetVersionTypeId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(SupplierBudgetVersionTypeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.SupplierBudgetVersionTypeId));
    }


}