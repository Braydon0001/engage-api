using Engage.Application.Services.AuditEntryProperties.Queries;

namespace Engage.WebApi.Controllers;

public partial class AuditEntryPropertyController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<AuditEntryPropertyDto>>> DtoList([FromQuery] AuditEntryPropertyListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<AuditEntryPropertyDto>(entities));
    }
}