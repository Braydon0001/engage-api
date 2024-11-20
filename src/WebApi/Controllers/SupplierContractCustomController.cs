using Engage.Application.Services.SupplierContracts.Queries;

namespace Engage.WebApi.Controllers;

public partial class SupplierContractController
{
    [HttpGet("supplierId/{supplierId}")]
    public async Task<ActionResult<IEnumerable<SupplierContractOption>>> OptionListBySupplier([FromQuery] SupplierContractOptionListQuery query, [FromRoute] int supplierId, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(new SupplierContractOptionListQuery { SupplierId = supplierId }, cancellationToken);

        return Ok(entities);
    }
}
