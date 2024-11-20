using Engage.Application.Services.EmployeeTransactionRemunerationTypes.Commands;
using Engage.Application.Services.EmployeeTransactionRemunerationTypes.Queries;

namespace Engage.WebApi.Controllers;

public partial class EmployeeTransactionRemunerationTypeController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<EmployeeTransactionRemunerationTypeDto>>> List([FromQuery] EmployeeTransactionRemunerationTypeListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<EmployeeTransactionRemunerationTypeDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<EmployeeTransactionRemunerationTypeOption>>> Options([FromQuery] EmployeeTransactionRemunerationTypeOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeTransactionRemunerationTypeVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new EmployeeTransactionRemunerationTypeVmQuery(id), cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(EmployeeTransactionRemunerationTypeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.EmployeeTransactionRemunerationTypeId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(EmployeeTransactionRemunerationTypeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.EmployeeTransactionRemunerationTypeId));
    }

}
