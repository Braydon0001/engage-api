using Engage.Application.Services.SupplierStores.Commands;
using Engage.Application.Services.SupplierStores.Models;
using Engage.Application.Services.SupplierStores.Queries;

namespace Engage.WebApi.Controllers.Mobile;

public class SupplierStoreController : BaseController
{
    [HttpGet("{id}")]
    public async Task<ActionResult<SupplierStoreVm>> Vm([FromRoute] SupplierStoreVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost("page")]
    public async Task<ActionResult<ListResult<SupplierStoreDto>>> Paginated([FromBody] SupplierStorePaginatedQuery query, [FromQuery ]int? supplierId)
    {
        query.SupplierId = supplierId;
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Insert(CreateSupplierStoreCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateSupplierStoreCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
