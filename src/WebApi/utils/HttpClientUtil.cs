using Engage.Application.Exceptions;
using System.Text;
using System.Text.Json;

namespace Engage.WebApi.utils;

public class HttpClientUtil
{
    public static async Task<T> SendPostRequestAsync<TRequest, T>(HttpClient httpClient, string requestUrl, TRequest requestData)
    {
        try
        {
            // Serialize the request payload to JSON then convert to string to easily transmit over the wire
            string jsonPayload = JsonSerializer.Serialize(requestData);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PostAsync(requestUrl, content);

            if (response.IsSuccessStatusCode)
            {
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    var responseObject = await JsonSerializer.DeserializeAsync<T>(responseStream);
                    //return new OkObjectResult(responseObject);
                    return responseObject;
                }
            }
            else
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var responseObject = await JsonSerializer.DeserializeAsync<T>(responseStream);
                //return new BadRequestObjectResult(responseObject);
                throw new BadHttpRequestException(responseObject.ToString());
            }
        }
        catch (Exception ex)
        {
            // Handle exceptions if needed
            //return new BadRequestObjectResult($"An error occurred: {ex.Message}");
            throw new BadHttpRequestException($"An error occured: {ex.Message}");
        }
    }

    public static async Task<T> SendPatchRequestAsync<TRequest, T>(HttpClient httpClient, string requestUrl, TRequest requestData)
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
                    var responseObject = await JsonSerializer.DeserializeAsync<T>(responseStream);
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
                    throw new BadHttpRequestException(responseObject.ToString());
                }
            }
        }
        catch (Exception ex)
        {
            // Handle exceptions if needed
            //return new BadRequestObjectResult($"An error occurred: {ex.Message}");
            throw new BadHttpRequestException($"An error occured: {ex.Message}");
        }
    }

    public static async Task<T> SendGetRequestAsync<T>(HttpClient httpClient, string requestUrl)
    {
        HttpResponseMessage response = await httpClient.GetAsync(requestUrl);

        if (response.IsSuccessStatusCode)
        {
            using var responseStream = await response.Content.ReadAsStreamAsync();
            var responseObject = await JsonSerializer.DeserializeAsync<T>(responseStream);
            return responseObject;
        }

        if (response.ReasonPhrase == "Not Found")
        {
            throw new NotFoundException($"Failed with status code {response.StatusCode}: {response.ReasonPhrase}", response);
        }

        throw new HttpRequestException($"Failed with status code {response.StatusCode}: {response.ReasonPhrase}");
    }
}
