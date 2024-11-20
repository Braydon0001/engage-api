// auto-generated
using Engage.Application.Services.SupplierBudgets.Commands;
using Engage.Application.Services.SupplierBudgets.Queries;

namespace Engage.WebApi.Controllers;

public partial class SupplierBudgetController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<SupplierBudgetDto>>> DtoList([FromQuery] SupplierBudgetListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<SupplierBudgetDto>(entities));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SupplierBudgetVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new SupplierBudgetVmQuery { Id = id }, cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(SupplierBudgetInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.SupplierBudgetId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(SupplierBudgetUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.SupplierBudgetId));
    }


}