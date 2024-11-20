using Engage.Application.Services.Claims.Rules.Models;

namespace Engage.Application.Services.Claims.Rules;

public static class InUserRegionRule
{   public static async Task<bool> Evaluate(ClaimRuleContext context, CancellationToken cancellationToken)
    {
        var result = false;

        if (context.User.IsHostSupplier && !!string.IsNullOrWhiteSpace(context.User.UserName))
        {

            var regions = await context.DbContext.Employees.Where(e => e.EmailAddress1.ToLower() == context.User.UserName.ToLower() &&
                                                                        e.Disabled == false)
                                                            .Join(context.DbContext.EmployeeRegions,
                                                                  employee => employee.EmployeeId,
                                                                  region => region.EmployeeId,
                                                                  (employee, region) => region.EngageRegion)
                                                            .ToListAsync(cancellationToken);
            if (regions.Any(e => e.IsAllRegions))
            {
                result = true;
            }
            else
            {
                var claimRegionId = await context.DbContext.Stores.Where(e => e.StoreId == context.Claim.StoreId)
                                                                   .Join(context.DbContext.EngageRegions,
                                                                          store => store.EngageRegionId,
                                                                          region => region.Id,
                                                                          (store, region) => region.Id)
                                                                   .SingleAsync(cancellationToken);

                if (regions.Select(e => e.Id).Contains(claimRegionId))
                {
                    result = true;
                }
            }
        }
        else
        {
            result = true;
        }

        return await Task.FromResult(result);

    }
}
