namespace Engage.Application.Interfaces;

//See https://blog.jeremylikness.com/blog/multitenancy-with-ef-core-in-blazor-server-apps/
public interface ITenantProvider
{
    void SetTenant(string tenant);

    string GetTenant();

    string GetTenantShortName();
}
