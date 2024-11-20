using System.Text;
using System.Text.Json;

namespace Engage.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MongoDbController : ControllerBase
{
    public class FormDef { }

    private static readonly HttpClient httpClient = new()
    {
        BaseAddress = new Uri("https://eu-west-2.aws.data.mongodb-api.com/app/data-sajwe/endpoint/data/v1/"),
    };

    [AllowAnonymous]
    [HttpGet("{name}")]
    public async Task<ActionResult> FindForm([FromRoute] string name)
    {
        using var request = new HttpRequestMessage(HttpMethod.Post, "action/findOne");

        request.Headers.Add("api-key", "T7F1NG5gZkaDmwHM3mNqHFsRkGvFUqhmwoDGtDuoHKz3nD1U8GaujxFiTYfzPqSh");

        using StringContent content = new(
        JsonSerializer.Serialize(new
        {
            dataSource = "Cluster0",
            database = "forms",
            collection = "forms",
            filter = new { name }
        }),
        Encoding.UTF8,
        "application/json");

        request.Content = content;

        var response = await httpClient.SendAsync(request);

        response.EnsureSuccessStatusCode();

        return Ok(await response.Content.ReadAsStringAsync());

    }
}
