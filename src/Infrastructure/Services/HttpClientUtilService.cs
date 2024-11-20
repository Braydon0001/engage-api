using System.Text;
using System.Text.Json;

namespace Engage.Infrastructure.Services;

public class HttpClientUtilService : IHttpClientUtilService
{
    public async Task<TResponse> SendGetRequestAsync<TResponse>(HttpClient httpClient, string requestUrl)
    {
        HttpResponseMessage response = await httpClient.GetAsync(requestUrl);

        if (response.IsSuccessStatusCode)
        {
            using var responseStream = await response.Content.ReadAsStreamAsync();
            var responseObject = await JsonSerializer.DeserializeAsync<TResponse>(responseStream);
            return responseObject;
        }

        if (response.ReasonPhrase == "Not Found")
        {
            throw new NotFoundException($"Failed with status code {response.StatusCode}: {response.ReasonPhrase}", response);
        }

        throw new HttpRequestException($"Failed with status code {response.StatusCode}: {response.ReasonPhrase}");
    }

    public async Task<TResponse> SendPatchRequestAsync<TRequest, TResponse>(HttpClient httpClient, string requestUrl, TRequest requestData)
    {
        try
        {
            // Serialize the request payload to JSON then convert to string to easily transmit over the wire
            string jsonPayload = JsonSerializer.Serialize(requestData);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PatchAsync(requestUrl, content);

            if (response.IsSuccessStatusCode)
            {
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    var responseObject = await JsonSerializer.DeserializeAsync<TResponse>(responseStream);
                    //return new OkObjectResult(responseObject);
                    return responseObject;
                }
            }
            else
            {
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    var responseObject = await JsonSerializer.DeserializeAsync<object>(responseStream);
                    //return new BadRequestObjectResult(responseObject);
                    throw new Exception(responseObject.ToString());
                }
            }
        }
        catch (Exception ex)
        {
            // Handle exceptions if needed
            //return new BadRequestObjectResult($"An error occurred: {ex.Message}");
            throw new Exception($"An error occured: {ex.Message}");
        }
    }

    public async Task<TResponse> SendPostRequestAsync<TRequest, TResponse>(HttpClient httpClient, string requestUrl, TRequest request)
    {
        HttpResponseMessage response = await httpClient.GetAsync(requestUrl);

        if (response.IsSuccessStatusCode)
        {
            using var responseStream = await response.Content.ReadAsStreamAsync();
            var responseObject = await JsonSerializer.DeserializeAsync<TResponse>(responseStream);
            return responseObject;
        }

        if (response.ReasonPhrase == "Not Found")
        {
            throw new NotFoundException($"Failed with status code {response.StatusCode}: {response.ReasonPhrase}", response);
        }

        throw new HttpRequestException($"Failed with status code {response.StatusCode}: {response.ReasonPhrase}");
    }
}
