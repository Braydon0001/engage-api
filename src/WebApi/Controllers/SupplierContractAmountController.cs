// auto-generated
using Engage.Application.Services.SupplierContractAmounts.Commands;
using Engage.Application.Services.SupplierContractAmounts.Queries;

namespace Engage.WebApi.Controllers;

public partial class SupplierContractAmountController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<SupplierContractAmountDto>>> DtoList([FromQuery] SupplierContractAmountListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<SupplierContractAmountDto>(entities));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SupplierContractAmountVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new SupplierContractAmountVmQuery { Id = id }, cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(SupplierContractAmountInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.SupplierContractAmountId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(SupplierContractAmountUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.SupplierContractAmountId));
    }

}