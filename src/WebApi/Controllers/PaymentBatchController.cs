using Engage.Application.Services.PaymentBatches.Commands;
using Engage.Application.Services.PaymentBatches.Queries;

namespace Engage.WebApi.Controllers;

public partial class PaymentBatchController : BaseController
{
    [HttpPost("page")]
    public async Task<ActionResult<ListResult<PaymentBatchDto>>> Paginated(PaymentBatchPaginatedQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<PaymentBatchDto>(entities));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PaymentBatchVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new PaymentBatchVmQuery(id), cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(PaymentBatchInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.PaymentBatchId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(PaymentBatchUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.PaymentBatchId));
    }

}
