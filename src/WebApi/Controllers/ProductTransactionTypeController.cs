// auto-generated
using Engage.Application.Services.ProductTransactionTypes.Commands;
using Engage.Application.Services.ProductTransactionTypes.Queries;

namespace Engage.WebApi.Controllers;

public partial class ProductTransactionTypeController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<ProductTransactionTypeDto>>> DtoList([FromQuery] ProductTransactionTypeListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<ProductTransactionTypeDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<ProductTransactionTypeOption>>> OptionList([FromQuery] ProductTransactionTypeOptionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("options/all")]
    public async Task<ActionResult<IEnumerable<ProductTransactionTypeOption>>> OptionFullList([FromQuery] ProductTransactionTypeOptionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(new ProductTransactionTypeOptionListQuery { ReturnAll = true }, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductTransactionTypeVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new ProductTransactionTypeVmQuery { Id = id }, cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(ProductTransactionTypeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.ProductTransactionTypeId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(ProductTransactionTypeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.ProductTransactionTypeId));
    }


}