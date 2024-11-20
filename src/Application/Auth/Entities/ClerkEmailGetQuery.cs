using System.Text.Json.Serialization;

namespace Engage.Application.Auth.Entities;

public class ClerkEmailGetQuery
{
    [JsonPropertyName("id")]
    public string Id { get; set; }
    [JsonPropertyName("email_address")]
    public string Email { get; set; }
    //[JsonPropertyName("")]
}
