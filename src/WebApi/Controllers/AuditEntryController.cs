using Engage.Application.Services.AuditEntries.Queries;

namespace Engage.WebApi.Controllers;

public partial class AuditEntryController : BaseController
{
    [HttpPost("page")]
    public async Task<ActionResult<ListResult<AuditEntryDto>>> Paginated(AuditEntryPaginatedQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<AuditEntryDto>(entities));
    }
}
