using Engage.Application.Interfaces;
using Engage.WebApi.utils;

namespace Engage.WebApi.Services;

public class ClerkHttpClient : IClerkHttpClient
{
    private readonly IHttpClientFactory _factory;

    public ClerkHttpClient(IHttpClientFactory factory)
    {
        _factory = factory;
    }

    public async Task<TResponse> PostAsync<TRequest, TResponse>(string url, TRequest request)
    {
        // Use the HttpClientFactory to create a named client with the name "Clerk"
        var client = _factory.CreateClient("Clerk");

        return await HttpClientUtil.SendPostRequestAsync<TRequest, TResponse>(client, url, request);
    }
}
