using Engage.Application.Services.CostCenterDepartments.Commands;
using Engage.Application.Services.CostCenterDepartments.Queries;

namespace Engage.WebApi.Controllers;

public partial class CostCenterDepartmentController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<CostCenterDepartmentDto>>> List([FromQuery] CostCenterDepartmentListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<CostCenterDepartmentDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<CostCenterDepartmentOption>>> Options([FromQuery] CostCenterDepartmentOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CostCenterDepartmentVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new CostCenterDepartmentVmQuery(id), cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(CostCenterDepartmentInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.CostCenterDepartmentId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(CostCenterDepartmentUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.CostCenterDepartmentId));
    }

}
