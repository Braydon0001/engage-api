using Newtonsoft.Json;

namespace Engage.Infrastructure.Models
{
    public class SendGridTemplateProperty
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
