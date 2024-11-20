namespace Engage.WebApi.Extensions;

public static class ClaimExtensions
{
    public static string GetClaimValue(this IEnumerable<System.Security.Claims.Claim> claims, string claimType)
    {
        if (claims != null && claims.Any())
        {
            return claims.Where(c => c.Type.ToLower() == claimType)
                         .Select(e => e.Value)
                         .FirstOrDefault();
        }
        return string.Empty;
    }

    public static bool HasClaimValue(this IEnumerable<System.Security.Claims.Claim> claims, string claimType)
    {
        return claims != null && claims.Any(c => c.Value.ToLower() == claimType);
    }
}
