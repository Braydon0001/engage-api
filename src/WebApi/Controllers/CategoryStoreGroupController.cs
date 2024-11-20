using Engage.Application.Services.CategoryStoreGroups.Commands;
using Engage.Application.Services.CategoryStoreGroups.Queries;

namespace Engage.WebApi.Controllers;

public partial class CategoryStoreGroupController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<CategoryStoreGroupDto>>> List([FromQuery] CategoryStoreGroupListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<CategoryStoreGroupDto>(entities));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryStoreGroupVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new CategoryStoreGroupVmQuery(id), cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(CategoryStoreGroupInsertCommand command, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus { Status = true, RecordsAffected = entities.Count, ReturnObject = entities.Select(e => e.CategoryStoreGroupId).ToList() });
    }

    [HttpPut]
    public async Task<IActionResult> Update(CategoryStoreGroupUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.CategoryStoreGroupId));
    }

}
