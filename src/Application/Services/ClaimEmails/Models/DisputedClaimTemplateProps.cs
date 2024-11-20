using Newtonsoft.Json;

namespace Engage.Application.Services.ClaimEmails.Models
{
    public class DisputedClaimTemplateProps : TemplateProps
    {
        [JsonProperty("approverName")]
        public string ApproverName { get; set; }

        [JsonProperty("disputedReason")]
        public string DisputedReason { get; set; }
    }
}
