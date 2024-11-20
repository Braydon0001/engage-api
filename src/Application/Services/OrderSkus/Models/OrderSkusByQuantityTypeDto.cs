using System.Collections.Generic;

namespace Engage.Application.Services.OrderSkus.Models
{
    public class OrderSkusByQuantityTypeDto 
    {
        public Dictionary<string, List<OrderSkuListItemDto>> ProductSkus { get; set; }
        public List<OrderSkuListItemDto> FreeTextSkus { get; set; }
    }
}
