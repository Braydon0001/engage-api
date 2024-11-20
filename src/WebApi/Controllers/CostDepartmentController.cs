using Engage.Application.Services.CostDepartments.Commands;
using Engage.Application.Services.CostDepartments.Queries;

namespace Engage.WebApi.Controllers;

public partial class CostDepartmentController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<CostDepartmentDto>>> List([FromQuery] CostDepartmentListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<CostDepartmentDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<CostDepartmentOption>>> Options([FromQuery] CostDepartmentOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CostDepartmentVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new CostDepartmentVmQuery(id), cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(CostDepartmentInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.CostDepartmentId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(CostDepartmentUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.CostDepartmentId));
    }

}
