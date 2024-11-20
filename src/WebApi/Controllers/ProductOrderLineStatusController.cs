using Engage.Application.Services.ProductOrderLineStatuses.Commands;
using Engage.Application.Services.ProductOrderLineStatuses.Queries;

namespace Engage.WebApi.Controllers;

public partial class ProductOrderLineStatusController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<ProductOrderLineStatusDto>>> List([FromQuery] ProductOrderLineStatusListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<ProductOrderLineStatusDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<ProductOrderLineStatusOption>>> Options([FromQuery] ProductOrderLineStatusOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductOrderLineStatusVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new ProductOrderLineStatusVmQuery(id), cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(ProductOrderLineStatusInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.ProductOrderLineStatusId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(ProductOrderLineStatusUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.ProductOrderLineStatusId));
    }

}
