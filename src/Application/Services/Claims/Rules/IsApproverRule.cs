using Engage.Application.Services.Claims.Rules.Models;

namespace Engage.Application.Services.Claims.Rules;

public static class IsApproverRule 
{
    
    public static async Task<bool> Evaluate(ClaimRuleContext context, CancellationToken _)
    {
        var result = false;
        if (!string.IsNullOrWhiteSpace(context.User.UserName))
        {
            result = context.User.HasClaimProcessClaim;
        }

        return await Task.FromResult(result);
    }
}
