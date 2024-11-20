using Engage.Application.Services.EngageBrands.Commands;
using Engage.Application.Services.EngageBrands.Queries;
using Engage.Application.Services.EngageCategories.Queries;

namespace Engage.WebApi.Controllers;

public partial class EngageBrandController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<EngageBrandDto>>> GetAll([FromQuery] EngageBrandsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("options/supplier")]
    public async Task<ActionResult<List<EngageBrandOption>>> OptionListBySupplier([FromQuery] EngageBrandBySupplierQuery query, CancellationToken cancellationToken)
    {
        var options = await Mediator.Send(query, cancellationToken);

        return Ok(options);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(EngageBrandInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.Id));
    }

    [HttpPut]
    public async Task<IActionResult> Update(EngageBrandUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.Id));
    }
}
