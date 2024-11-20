using Engage.Application.Services.ProjectFileTypes.Commands;
using Engage.Application.Services.ProjectFileTypes.Queries;

namespace Engage.WebApi.Controllers;

public partial class ProjectFileTypeController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<ProjectFileTypeDto>>> DtoList([FromQuery] ProjectFileTypeListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<ProjectFileTypeDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<ProjectFileTypeOption>>> OptionList([FromQuery] ProjectFileTypeOptionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectFileTypeVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new ProjectFileTypeVmQuery { Id = id }, cancellationToken);
        if (entity == null)
        {
            return NotFound();
        }

        return entity;
    }

    [HttpPost]
    public async Task<IActionResult> Insert(ProjectFileTypeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.ProjectFileTypeId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(ProjectFileTypeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.ProjectFileTypeId));
    }
}