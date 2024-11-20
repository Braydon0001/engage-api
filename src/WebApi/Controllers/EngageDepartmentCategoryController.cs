// auto-generated
using Engage.Application.Services.EngageDepartmentCategories.Commands;
using Engage.Application.Services.EngageDepartmentCategories.Queries;

namespace Engage.WebApi.Controllers;

public partial class EngageDepartmentCategoryController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<EngageDepartmentCategoryDto>>> DtoList([FromQuery] EngageDepartmentCategoryListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<EngageDepartmentCategoryDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<EngageDepartmentCategoryOption>>> OptionList([FromQuery] EngageDepartmentCategoryOptionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EngageDepartmentCategoryVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new EngageDepartmentCategoryVmQuery { Id = id }, cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(EngageDepartmentCategoryInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.Id));
    }

    [HttpPut]
    public async Task<IActionResult> Update(EngageDepartmentCategoryUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.Id));
    }


}