using Engage.Application.Services.CategoryGroups.Commands;
using Engage.Application.Services.CategoryGroups.Queries;

namespace Engage.WebApi.Controllers.Mobile;

public partial class CategoryGroupController : BaseMobileController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<CategoryGroupDto>>> List([FromQuery] CategoryGroupListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<CategoryGroupDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<CategoryGroupOption>>> Options([FromQuery] CategoryGroupOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryGroupVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new CategoryGroupVmQuery(id), cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(CategoryGroupInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.CategoryGroupId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(CategoryGroupUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.CategoryGroupId));
    }

}
