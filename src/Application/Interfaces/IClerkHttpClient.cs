namespace Engage.Application.Interfaces;

public interface IClerkHttpClient
{
    Task<TResponse> PostAsync<TRequest, TResponse>(string url, TRequest request);
}
