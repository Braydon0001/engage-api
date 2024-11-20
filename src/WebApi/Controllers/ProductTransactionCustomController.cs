using Engage.Application.Services.ProductTransactions.Commands;
using Engage.Application.Services.ProductTransactions.Queries;

namespace Engage.WebApi.Controllers;

public partial class ProductTransactionController : BaseController
{
    [HttpGet("employeeId/{employeeId}")]
    public async Task<ActionResult<ListResult<ProductTransactionDto>>> ListByEmployee([FromRoute] ProductTransactionByEmployeeQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);
        return Ok(new ListResult<ProductTransactionDto>(entities));
    }

    [HttpPost("bulkInsert")]
    public async Task<IActionResult> InsertBulk(ProductTransactionBulkInsertCommand command, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(command, cancellationToken);
        return Ok(new OperationStatus
        {
            Status = true,
            RecordsAffected = entities.Count,
            ReturnObject = entities
        });
    }
}
