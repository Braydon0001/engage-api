// auto-generated
using Engage.Application.Services.ProductTransactionStatuses.Commands;
using Engage.Application.Services.ProductTransactionStatuses.Queries;

namespace Engage.WebApi.Controllers;

public partial class ProductTransactionStatusController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<ProductTransactionStatusDto>>> DtoList([FromQuery]ProductTransactionStatusListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<ProductTransactionStatusDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<ProductTransactionStatusOption>>> OptionList([FromQuery]ProductTransactionStatusOptionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductTransactionStatusVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new ProductTransactionStatusVmQuery { Id = id }, cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(ProductTransactionStatusInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.ProductTransactionStatusId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(ProductTransactionStatusUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.ProductTransactionStatusId));
    }


}