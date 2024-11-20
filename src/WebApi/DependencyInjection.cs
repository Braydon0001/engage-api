using System.Text.Json;

namespace Engage.WebApi;

public static class DependencyInjection
{
    public static IServiceCollection AddAuthorizationPolicies(this IServiceCollection services, bool isClerkAuthentication)
    {
        var policyNames = new List<string> {
            "admin",
            "asset",
            "budget",
            "claim",
            "contact",
            "dc",
            "employee",
            "ensource",
            "generalledger",
            "location",
            "notification",
            "order",
            "order2",
            "product",
            "store",
            "supplier",
            "survey",
            "mobile"
        };

        //var clerkPolicyNames = new List<string>
        //{
        //    "order:create",
        //    "order:process",
        //    "order:view",
        //    "order:manage",
        //    "order",
        //    "product",
        //    "admin",
        //    "employee"
        //};

        if (isClerkAuthentication)
        {
            services.AddAuthorization(options =>
            {
                policyNames.ForEach(policyName =>
                {
                    options.AddPolicy(policyName, policy => policy.RequireAssertion(context =>
                    {
                        var publicMetaClaim = context.User.Claims.FirstOrDefault(c => c.Type.Equals("publicMeta"))?.Value;
                        if (!string.IsNullOrWhiteSpace(publicMetaClaim))
                        {
                            var publicMeta = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(publicMetaClaim);
                            if (publicMeta is not null && publicMeta.TryGetValue("roles", out List<string> roles))
                            {
                                return roles.Contains(policyName);
                            }
                        }
                        return false;
                    }));
                });
            });
        }
        else
        {
            services.AddAuthorization(options =>
            {
                policyNames.ForEach(policyName =>
                    options.AddPolicy(policyName, policy => policy.RequireClaim(policyName)));
            });
        }

        return services;
    }
}
