using Engage.Application.Services.Claims.Rules.Models;

namespace Engage.Application.Services.Claims.Rules;

public class MustApproveRule
{
    public static async Task<bool> Evaluate(ClaimRuleContext context, CancellationToken cancellationToken)
    {
        return await context.DbContext.Stores.Where(e => e.StoreId == context.Claim.StoreId)
                                             .Join(context.DbContext.EngageRegions,
                                                   store => store.EngageRegionId,
                                                   region => region.Id,
                                                   (store, region) => region.IsApproveClaims)
                                             .SingleAsync(cancellationToken);
    }
}
