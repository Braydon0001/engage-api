using Engage.Application.Services.ProjectSubCategories.Commands;
using Engage.Application.Services.ProjectSubCategories.Queries;

namespace Engage.WebApi.Controllers.Mobile;

public partial class ProjectSubCategoryController : BaseMobileController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<ProjectSubCategoryDto>>> List([FromQuery] ProjectSubCategoryListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<ProjectSubCategoryDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<ProjectSubCategoryOption>>> Options([FromQuery] ProjectSubCategoryOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectSubCategoryVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new ProjectSubCategoryVmQuery(id), cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(ProjectSubCategoryInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.ProjectSubCategoryId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(ProjectSubCategoryUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.ProjectSubCategoryId));
    }

}
