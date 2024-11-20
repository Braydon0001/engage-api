using Engage.Application.Services.CategoryTargetTypes.Commands;
using Engage.Application.Services.CategoryTargetTypes.Queries;

namespace Engage.WebApi.Controllers;

public partial class CategoryTargetTypeController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<CategoryTargetTypeDto>>> List([FromQuery] CategoryTargetTypeListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<CategoryTargetTypeDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<CategoryTargetTypeOption>>> Options([FromQuery] CategoryTargetTypeOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryTargetTypeVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new CategoryTargetTypeVmQuery(id), cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(CategoryTargetTypeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.CategoryTargetTypeId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(CategoryTargetTypeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.CategoryTargetTypeId));
    }

}
