using Engage.Application.Services.ExpenseTypes.Commands;
using Engage.Application.Services.ExpenseTypes.Queries;

namespace Engage.WebApi.Controllers;

public partial class ExpenseTypeController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<ExpenseTypeDto>>> List([FromQuery] ExpenseTypeListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<ExpenseTypeDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<ExpenseTypeOption>>> Options([FromQuery] ExpenseTypeOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ExpenseTypeVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new ExpenseTypeVmQuery(id), cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(ExpenseTypeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.ExpenseTypeId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(ExpenseTypeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.ExpenseTypeId));
    }

}
