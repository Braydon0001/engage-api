using System.Text.Json.Serialization;

namespace Engage.Application.Auth.Entities;

public class ClerkUserEmailUpdateCommand
{
    [JsonPropertyName("user_id")]
    public string ExternalId { get; set; }
    [JsonPropertyName("email_address")]
    public string Email { get; set; }
    [JsonPropertyName("verified")]
    public bool Verified { get; set; } = true;
    [JsonPropertyName("primary")]
    public bool Primary { get; set; } = true;
}
