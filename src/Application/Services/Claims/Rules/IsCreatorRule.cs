using Engage.Application.Services.Claims.Rules.Models;

namespace Engage.Application.Services.Claims.Rules;

public class IsCreatorRule 
{
    public static async Task<bool> Evaluate(ClaimRuleContext context, CancellationToken _)
    {
        var createdBy = context.Claim.CreatedBy;
        var userName = context.User.UserName;

        var result = false;
        if (!string.IsNullOrWhiteSpace(createdBy) &&
            !string.IsNullOrWhiteSpace(userName) &&
            createdBy.ToLower().Equals(userName.ToLower()))
        {
            result = true;
        }

        return await Task.FromResult(result);
    }
}
