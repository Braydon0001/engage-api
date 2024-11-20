using Engage.Application.Services.ProjectTypes.Commands;
using Engage.Application.Services.ProjectTypes.Queries;

namespace Engage.WebApi.Controllers;

public partial class ProjectTypeController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<ProjectTypeDto>>> List([FromQuery] ProjectTypeListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<ProjectTypeDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<ProjectTypeOption>>> Options([FromQuery] ProjectTypeOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectTypeVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new ProjectTypeVmQuery(id), cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(ProjectTypeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.ProjectTypeId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(ProjectTypeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.ProjectTypeId));
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(ProjectTypeDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

}
