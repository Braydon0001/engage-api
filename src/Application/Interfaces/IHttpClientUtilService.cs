namespace Engage.Application.Interfaces;

public interface IHttpClientUtilService
{
    Task<TResponse> SendGetRequestAsync<TResponse>(HttpClient httpClient, string requestUrl);
    Task<TResponse> SendPostRequestAsync<TRequest, TResponse>(HttpClient httpClient, string requestUrl, TRequest requestData);
    Task<TResponse> SendPatchRequestAsync<TRequest, TResponse>(HttpClient httpClient, string requestUri, TRequest request);
}
