using Engage.Application.Services.Settings.Commands;
using Engage.Application.Services.Settings.Models;
using Engage.Application.Services.Settings.Queries;

namespace Engage.WebApi.Controllers;

public class SupplierSettingController : BaseController
{
    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<ListResult<SupplierSettingDto>>> GetSupplierSettings([FromRoute] SupplierSettingsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
    
    [AllowAnonymous]
    [HttpGet("supplierId/{supplierId}")]
    public async Task<ActionResult<ListResult<SupplierSettingDto>>> GetSupplierSettingsBySupplier([FromRoute] SupplierSettingsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost("upsert")]
    public async Task<ActionResult<SupplierSettingDto>> UpsertSupplierSetting(UpsertSupplierSettingCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
