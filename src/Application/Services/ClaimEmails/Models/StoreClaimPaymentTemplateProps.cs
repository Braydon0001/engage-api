using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engage.Application.Services.ClaimEmails.Models
{
    public class StoreClaimPaymentTemplateProps: TemplateProps
    {
        public StoreClaimPaymentTemplateProps()
        {
            ClaimNumbers = new List<ClaimNumber>();
        }

        [JsonProperty("totalAmount")]
        public decimal TotalAmount { get; set; }

        [JsonProperty("storeName")]
        public string StoreName { get; set; }

        [JsonProperty("claimNumbers")]
        public List<ClaimNumber> ClaimNumbers { get; set; }
    }

    public class ClaimNumber
    {
        [JsonProperty("claimNo")]
        public string ClaimNo { get; set; }
        [JsonProperty("amount")]
        public decimal Amount { get; set; }
    }
}
