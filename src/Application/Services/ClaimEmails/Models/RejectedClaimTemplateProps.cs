using Newtonsoft.Json;

namespace Engage.Application.Services.ClaimEmails.Models
{
    public class RejectedClaimTemplateProps: TemplateProps
    {
        [JsonProperty("approverName")]
        public string ApproverName { get; set; }

        [JsonProperty("rejectedReason")]
        public string RejectedReason { get; set; }
    }
}
