using Engage.Application.Services.SupplierContractAmounts.Commands;
using Engage.Application.Services.SupplierContractAmounts.Queries;

namespace Engage.WebApi.Controllers;

public partial class SupplierContractAmountController : BaseController
{
    [HttpGet("supplierSubContractDetailId/{supplierSubContractDetailId}")]
    public async Task<ActionResult<ListResult<SupplierContractAmountDto>>> ListByContractDetail([FromRoute] SupplierContractAmountByContractDetailQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<SupplierContractAmountDto>(entities));
    }

    [HttpPut("disable/{id}")]
    public async Task<IActionResult> Disable([FromRoute] SupplierContractAmountDisableCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);
        return Ok(new OperationStatus(entity.SupplierContractAmountId));
    }
}
