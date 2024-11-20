using Engage.Application.Services.EmployeeTransactionTypeGroups.Queries;

namespace Engage.WebApi.Controllers;

public partial class EmployeeTransactionTypeGroupController : BaseController
{
    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<EmployeeTransactionTypeGroupOption>>> OptionList([FromQuery] EmployeeTransactionTypeGroupOptionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }
}