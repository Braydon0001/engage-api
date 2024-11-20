using Engage.Application.Services.ProjectCategories.Commands;
using Engage.Application.Services.ProjectCategories.Queries;

namespace Engage.WebApi.Controllers;

public partial class ProjectCategoryController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<ProjectCategoryDto>>> List([FromQuery] ProjectCategoryListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<ProjectCategoryDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<ProjectCategoryOption>>> Options([FromQuery] ProjectCategoryOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectCategoryVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new ProjectCategoryVmQuery(id), cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(ProjectCategoryInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.ProjectCategoryId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(ProjectCategoryUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.ProjectCategoryId));
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(ProjectCategoryDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

}
