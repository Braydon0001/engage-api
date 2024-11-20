using Engage.Application.Services.Tenant.Queries;

namespace Engage.WebApi.Controllers;
public class TenantController : BaseController
{
    [HttpGet("supplier")]
    public async Task<ActionResult<OptionDto>> TenantSupplier([FromRoute] TenantSupplierQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
}
