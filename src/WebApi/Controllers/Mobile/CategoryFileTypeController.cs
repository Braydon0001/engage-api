using Engage.Application.Services.CategoryFileTypes.Commands;
using Engage.Application.Services.CategoryFileTypes.Queries;

namespace Engage.WebApi.Controllers.Mobile;

public partial class CategoryFileTypeController : BaseMobileController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<CategoryFileTypeDto>>> List([FromQuery] CategoryFileTypeListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<CategoryFileTypeDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<CategoryFileTypeOption>>> Options([FromQuery] CategoryFileTypeOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryFileTypeVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new CategoryFileTypeVmQuery(id), cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(CategoryFileTypeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.CategoryFileTypeId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(CategoryFileTypeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.CategoryFileTypeId));
    }

}
