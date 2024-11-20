using Engage.Application.Services.CategoryTargets.Commands;
using Engage.Application.Services.CategoryTargets.Queries;

namespace Engage.WebApi.Controllers.Mobile;

public partial class CategoryTargetController : BaseMobileController
{
    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryTargetVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new CategoryTargetVmQuery(id), cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }


    //[HttpGet("storeId/{storeId}")]
    //public async Task<ActionResult<ListResult<CategoryTargetDto>>> GetByStore([FromRoute] CategoryTargetByStoreQuery query, CancellationToken cancellationToken)
    //{
    //    var entities = await Mediator.Send(query, cancellationToken);

    //    return Ok(new ListResult<CategoryTargetDto>(entities));
    //}

    [HttpGet]
    public async Task<ActionResult<ListResult<CategoryTargetDto>>> List([FromQuery] CategoryTargetQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<CategoryTargetDto>(entities));
    }

    [HttpPost("page")]
    public async Task<ActionResult<ListResult<CategoryTargetDto>>> Paginated(CategoryTargetPaginatedQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<CategoryTargetDto>(entities));
    }

    [HttpPost]
    public async Task<IActionResult> Insert(CategoryTargetInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.CategoryTargetId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(CategoryTargetUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.CategoryTargetId));
    }

}
