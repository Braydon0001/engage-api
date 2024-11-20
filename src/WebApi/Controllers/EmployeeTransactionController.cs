// auto-generated
using Engage.Application.Services.EmployeeTransactions.Queries;

namespace Engage.WebApi.Controllers;

public partial class EmployeeTransactionController : BaseController
{
    [HttpGet("page")]
    public async Task<ActionResult<ListResult<EmployeeTransactionDto>>> PaginatedList([FromQuery] EmployeeTransactionPaginatedListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<EmployeeTransactionDto>(entities));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeTransactionVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new EmployeeTransactionVmQuery { Id = id }, cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    //[HttpPost]
    //public async Task<IActionResult> Insert(EmployeeTransactionInsertCommand command, CancellationToken cancellationToken)
    //{
    //    var entity = await Mediator.Send(command, cancellationToken);

    //    return Ok(new OperationStatus(entity.EmployeeTransactionId));
    //}

    //[HttpPut]
    //public async Task<IActionResult> Update(EmployeeTransactionUpdateCommand command, CancellationToken cancellationToken)
    //{
    //    var entity = await Mediator.Send(command, cancellationToken);

    //    return entity == null ? NotFound() : Ok(new OperationStatus(entity.EmployeeTransactionId));
    //}


}