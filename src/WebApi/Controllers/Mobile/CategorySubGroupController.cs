using Engage.Application.Services.CategorySubGroups.Commands;
using Engage.Application.Services.CategorySubGroups.Queries;

namespace Engage.WebApi.Controllers.Mobile;

public partial class CategorySubGroupController : BaseMobileController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<CategorySubGroupDto>>> List([FromQuery] CategorySubGroupListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<CategorySubGroupDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<CategorySubGroupOption>>> Options([FromQuery] CategorySubGroupOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategorySubGroupVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new CategorySubGroupVmQuery(id), cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(CategorySubGroupInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.CategorySubGroupId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(CategorySubGroupUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.CategorySubGroupId));
    }

}
