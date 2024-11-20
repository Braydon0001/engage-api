// auto-generated
using Engage.Application.Services.ProductReasons.Commands;
using Engage.Application.Services.ProductReasons.Queries;

namespace Engage.WebApi.Controllers;

public partial class ProductReasonController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<ProductReasonDto>>> DtoList([FromQuery]ProductReasonListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<ProductReasonDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<ProductReasonOption>>> OptionList([FromQuery]ProductReasonOptionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductReasonVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new ProductReasonVmQuery { Id = id }, cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(ProductReasonInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.ProductReasonId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(ProductReasonUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.ProductReasonId));
    }


}