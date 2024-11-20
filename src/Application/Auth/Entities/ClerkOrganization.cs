using System.Text.Json.Serialization;

namespace Engage.Application.Auth.Entities;

public class ClerkOrganization
{
    [JsonPropertyName("object")]
    public string Object { get; set; }
    [JsonPropertyName("id")]
    public string Id { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("slug")]
    public string Slug { get; set; }
    [JsonPropertyName("members_count")]
    public int MemberCount { get; set; }
    [JsonPropertyName("max_allowed_memberships")]
    public int MaxMemberships { get; set; }
    [JsonPropertyName("admin_delete_enabled")]
    public bool AdminDelete { get; set; }

    //[JsonPropertyName("public_metadata")]
    //public PublicMetaData {get; set;}
    //[JsonPropertyName("private_metadata")]
    //public PrivateMetaData {get; set;}

    [JsonPropertyName("created_by")]
    public string CreatedBy { get; set; }

}
