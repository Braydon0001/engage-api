using System.Text.Json.Serialization;

namespace Engage.Application.Auth.Entities;

public class UserPublicMeta
{
    [JsonPropertyName("public_metadata")]
    public UserPublicMetadata Metadata { get; set; }
}

public class UserPublicMetadata
{
    [JsonPropertyName("roles")]
    public List<string> Roles { get; set; }
    [JsonPropertyName("permissions")]
    public List<string> Permissions { get; set; }
}