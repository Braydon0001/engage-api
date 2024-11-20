using Newtonsoft.Json;

namespace Engage.Application.Services.ClaimEmails.Models
{
    public class TemplateProps
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("claimNumber")]
        public string ClaimNumber { get; set; }
    }
}
