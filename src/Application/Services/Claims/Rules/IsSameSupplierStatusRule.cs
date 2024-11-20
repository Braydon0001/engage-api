using Engage.Application.Services.Claims.Rules.Models;

namespace Engage.Application.Services.Claims.Rules;

public static class IsSameSupplierStatusRule 
{
    public static async Task<bool> Evaluate(int newClaimSupplierStatus, ClaimRuleContext context, CancellationToken _)
    {
        var result = context.Claim.ClaimSupplierStatusId == newClaimSupplierStatus;
        return await Task.FromResult(result);

    }
}
