using Engage.Application.Services.CostSubDepartments.Commands;
using Engage.Application.Services.CostSubDepartments.Queries;

namespace Engage.WebApi.Controllers;

public partial class CostSubDepartmentController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<CostSubDepartmentDto>>> List([FromQuery] CostSubDepartmentListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<CostSubDepartmentDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<CostSubDepartmentOption>>> Options([FromQuery] CostSubDepartmentOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CostSubDepartmentVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new CostSubDepartmentVmQuery(id), cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(CostSubDepartmentInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.CostSubDepartmentId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(CostSubDepartmentUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.CostSubDepartmentId));
    }

}
