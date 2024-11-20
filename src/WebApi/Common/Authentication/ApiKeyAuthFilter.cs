using Engage.Application.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Engage.WebApi.Common.Authentication;

public class ApiKeyAuthFilter(IAppDbContext context) : IAsyncAuthorizationFilter //IAuthorizationFilter
{
    #region Use AppSettings
    //private readonly IConfiguration _configuration;
    //public ApiKeyAuthFilter(IConfiguration configuration)
    //{
    //    _configuration = configuration;
    //}
    //public void OnAuthorization(AuthorizationFilterContext context)
    //{
    //    if (!context.HttpContext.Request.Headers.TryGetValue(AuthConstants.ApiKeyHeaderName, out var extractedApiKey))
    //    {
    //        context.Result = new UnauthorizedObjectResult("API Key is missing");
    //        return;
    //    }

    //    var apiKey = _configuration.GetValue<string>(AuthConstants.ApiKeySectionName);

    //    if (!apiKey.Equals(extractedApiKey))
    //    {
    //        context.Result = new UnauthorizedObjectResult("Unauthorized client");
    //        return;
    //    }
    //}
    #endregion

    #region Use Database
    private readonly IAppDbContext _context = context;

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.Request.Headers.TryGetValue(AuthConstants.ApiKeyHeaderName, out var extractedApiKey))
        {
            context.Result = new UnauthorizedObjectResult("API Key is missing");
            return;
        }

        var extractedKey = extractedApiKey.ToString();
        var key = await _context.ApiKeys.Where(e => e.Value == extractedKey)
                                        .FirstOrDefaultAsync();

        if (key == null)
        {
            context.Result = new UnauthorizedObjectResult("Unauthorized client");
            return;
        }
    }
    #endregion
}
