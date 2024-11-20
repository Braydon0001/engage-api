using Engage.Application.Services.CategoryTargetStores.Commands;
using Engage.Application.Services.CategoryTargetStores.Queries;

namespace Engage.WebApi.Controllers;

public partial class CategoryTargetStoreController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<CategoryTargetStoreDto>>> List([FromQuery] CategoryTargetStoreListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<CategoryTargetStoreDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<CategoryTargetStoreOption>>> Options([FromQuery] CategoryTargetStoreOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryTargetStoreVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new CategoryTargetStoreVmQuery(id), cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(CategoryTargetStoreInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.CategoryTargetStoreId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(CategoryTargetStoreUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.CategoryTargetStoreId));
    }

}
