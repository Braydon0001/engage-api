using Engage.Application.Services.CostCenterEmployees.Commands;
using Engage.Application.Services.CostCenterEmployees.Queries;

namespace Engage.WebApi.Controllers;

public partial class CostCenterEmployeeController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<CostCenterEmployeeDto>>> List([FromQuery] CostCenterEmployeeListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<CostCenterEmployeeDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<CostCenterEmployeeOption>>> Options([FromQuery] CostCenterEmployeeOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CostCenterEmployeeVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new CostCenterEmployeeVmQuery(id), cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(CostCenterEmployeeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.CostCenterEmployeeId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(CostCenterEmployeeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

}
