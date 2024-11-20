using Engage.Application.Services.Organizations.Commands;
using Engage.Application.Services.Organizations.Queries;
using Engage.Application.Services.OrganizationSettings.Queries;

namespace Engage.WebApi.Controllers;

public partial class OrganizationSettingController : BaseController
{
    [HttpGet("organizations/{organizationId}/settings")]
    public async Task<ActionResult<OrganizationWithSettingVm>> GetOrganizationSettingByOrganizationId([FromRoute] int organizationId, CancellationToken cancellationToken)
    {
        if (organizationId <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new OrganizationSettingVmQuery(organizationId), cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPut]
    public async Task<IActionResult> Upsert(OrganizationSettingsUpsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.OrganizationSetting.OrganizationSettingId));
    }
}
