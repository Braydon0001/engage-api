using Engage.Application.Services.ClaimEmails.Commands;
using Engage.Application.Services.ClaimEmails.Models;
using Engage.Application.Services.Claims.Queries;

namespace Engage.WebApi.Controllers;

public class ClaimEmailController : BaseController
{
    [HttpGet("claimperiodid/{claimperiodid}")]    
    public async Task<ActionResult<ListResult<ClaimAccountManagersToRemindDto>>> GetClaimAccountManagersToReminder([FromRoute] int? claimPeriodId)
    {
        return Ok(await Mediator.Send(new ClaimAccountManagersToRemindQuery
        {
            ClaimPeriodId = claimPeriodId,
        }));
    }

    [HttpPost("SendClaimApprovalReminder")]
    public async Task<IActionResult> ClaimApprovalReminder([FromBody] SendClaimApprovalReminderCommand command)
    {
        return Ok(await Mediator.Send(command));
    }    
}
