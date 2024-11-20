using Engage.Application.Services.Claims.Rules.Models;

namespace Engage.Application.Services.Claims.Rules;

public static class InApprovalPeriodRule 
{
    public static async Task<bool> Evaluate(ClaimRuleContext context, CancellationToken cancellationToken)
    {
        var today = DateTime.Now;
        var currentPeriod = await context.DbContext.ClaimPeriods.Where(e => today.Date >= e.StartDate.Date && today.Date <= e.EndDate.Date)
                                                                 .SingleOrDefaultAsync(cancellationToken);

        if (context.Claim.ApprovedDate.HasValue && currentPeriod != null)
        {
            var approvedDate = (DateTime)context.Claim.ApprovedDate;
            var approvedPeriod = await context.DbContext.ClaimPeriods.Where(e => approvedDate.Date >= e.StartDate.Date && approvedDate.Date <= e.EndDate.Date)
                                                                     .SingleOrDefaultAsync(cancellationToken);
            if (approvedPeriod != null)
            {
                return currentPeriod.ClaimPeriodId == approvedPeriod.ClaimPeriodId;
            }
        }

        return false;
    }
}
