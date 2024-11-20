using System.Text.Json.Serialization;

namespace Engage.Application.Services.SecurityOrganizations.Commands;

public class SecurityOrganizationClerkCreateCommand
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("created_by")]
    public string UserId { get; set; }
    //[JsonPropertyName("public_metadata")]
    //public PublicMetaData {get; set;}
    //[JsonPropertyName("private_metadata")]
    //public PrivateMetaData {get; set;}
    [JsonPropertyName("slug")]
    public string Slug { get; set; }
    [JsonPropertyName("max_allowed_memberships")]
    public int MembershipMax { get; set; }
}
