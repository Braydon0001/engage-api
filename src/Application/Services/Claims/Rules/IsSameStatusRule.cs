using Engage.Application.Services.Claims.Rules.Models;

namespace Engage.Application.Services.Claims.Rules;

public static class IsSameStatusRule 
{
    public static async Task<bool> Evaluate(int newClaimStatus, ClaimRuleContext context, CancellationToken _)
    {
        var result = context.Claim.ClaimStatusId == newClaimStatus;
        return await Task.FromResult(result);
    }
}
