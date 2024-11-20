using Engage.Application.Interfaces;
using Engage.Application.Services.Users.Queries;

namespace Engage.WebApi.Services;

public class UserService : IUserService
{
    private readonly IMediator _mediator;

    public UserService(IHttpContextAccessor httpContextAccessor, IOptions<List<SupplierClaim>> supplierClaims, IOptions<List<OrganizationClaim>> organizationClaims, IMediator mediator)
    {
        _mediator = mediator;

        var claims = httpContextAccessor.HttpContext?.User?.Claims;

        //if (isClerkAuthentication)
        //{
        //    PrimaryEmail = claims.FirstOrDefault(c => c.Type.Equals("primaryEmail"))?.Value;

        //    var externalId = claims.FirstOrDefault(c => c.Type.Equals("externalId"))?.Value;
        //    if (!string.IsNullOrWhiteSpace(externalId) && int.TryParse(externalId, out var userId))
        //    {
        //        UserId = userId;
        //    }

        //    //TODO supplierId use Organization metadata

        //    var organizationsClaim = claims.FirstOrDefault(c => c.Type.Equals("organizations"))?.Value;
        //    if (!string.IsNullOrWhiteSpace(organizationsClaim))
        //    {
        //        var organizations = JsonSerializer.Deserialize<Dictionary<string, string>>(organizationsClaim);
        //        if (organizations is not null)
        //        {
        //            Organizations = organizations;
        //        }
        //    }

        //    var publicMetaClaim = claims.FirstOrDefault(c => c.Type.Equals("publicMeta"))?.Value;
        //    if (!string.IsNullOrWhiteSpace(publicMetaClaim))
        //    {
        //        var publicMeta = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(publicMetaClaim);
        //        if (publicMeta is not null && publicMeta.TryGetValue("permissions", out List<string> permissions))
        //        {
        //            Permissions = permissions;
        //        }
        //    }
        //}

        UserName = claims.GetClaimValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");

        var organization = claims.GetClaimValue("_organization");
        organization = string.IsNullOrWhiteSpace(organization) ? "engage" : organization.Trim().ToLower();
        IsHostOrganization = organization.Equals("engage");

        var organizationClaim = organizationClaims.Value.Where(e => e.Claim.ToLower() == organization).FirstOrDefault();
        if (organizationClaim != null)
        {
            ConnectionString = organizationClaim.ConnectionString;
        }

        var supplier = claims.GetClaimValue("_supplier");
        supplier = string.IsNullOrWhiteSpace(supplier) ? "engage" : supplier.Trim().ToLower();
        IsHostSupplier = supplier.Equals("engage");

        var supplierClaim = supplierClaims.Value.Where(e => e.Claim.ToLower() == supplier).FirstOrDefault();
        if (supplierClaim != null)
        {
            SupplierId = supplierClaim.SupplierId;
        }

        HasClaimProcessClaim = claims.HasClaimValue("claim.process");
        HasClaimProcessAfterCutOffClaim = claims.HasClaimValue("claim.processaftercutoff");
        HasClaimEditAfterCutOffClaim = claims.HasClaimValue("claim.editaftercutoff");
        HasTrainingAdminClaim = claims.HasClaimValue("admin.training");
    }

    //public string PrimaryEmail { get; }
    //public int UserId { get; }
    //public Dictionary<string, string> Organizations { get; set; }
    //public List<string> Permissions { get; set; }
    public string UserName { get; }
    public bool IsHostOrganization { get; }
    public string ConnectionString { get; }
    public bool IsHostSupplier { get; }
    public int SupplierId { get; }
    public bool HasClaimProcessClaim { get; }
    public bool HasClaimProcessAfterCutOffClaim { get; }
    public bool HasClaimEditAfterCutOffClaim { get; }
    public bool HasTrainingAdminClaim { get; }

    public async Task<int> GetUserIdAsync()
    {
        return await _mediator.Send(new UserIdQuery());
    }

    //public bool HasPermission(string permission)
    //{
    //    return !string.IsNullOrWhiteSpace(permission) && Permissions is not null && Permissions.Contains(permission);
    //}
}
