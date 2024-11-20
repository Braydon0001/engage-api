using Engage.Application.Services.ProjectSubTypes.Commands;
using Engage.Application.Services.ProjectSubTypes.Queries;

namespace Engage.WebApi.Controllers;

public partial class ProjectSubTypeController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<ProjectSubTypeDto>>> List([FromQuery] ProjectSubTypeListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<ProjectSubTypeDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<ProjectSubTypeOption>>> Options([FromQuery] ProjectSubTypeOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectSubTypeVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new ProjectSubTypeVmQuery(id), cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(ProjectSubTypeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.ProjectSubTypeId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(ProjectSubTypeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.ProjectSubTypeId));
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(ProjectSubTypeDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

}
