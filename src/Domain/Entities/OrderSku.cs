namespace Engage.Domain.Entities
{
    public class OrderSku : BaseAuditableEntity
    {
        public int OrderSkuId { get; set; }
        public int OrderId { get; set; }
        public int OrderSkuTypeId { get; set; }
        public int OrderSkuStatusId { get; set; }
        public int DCProductId { get; set; }
        public int OrderQuantityTypeId { get; set; }
        public int? OrderTemplateProductId { get; set; }
        public int Quantity { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal PromotionPrice { get; set; }
        public decimal RecommendedPrice { get; set; }
        public decimal GrossProfitPercent { get; set; }
        public string Suffix { get; set; }
        public string Note { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public List<JsonFile> Files { get; set; }

        // Navigation Properties
        public Order Order { get; set; }
        public OrderSkuType OrderSkuType { get; set; }
        public OrderSkuStatus OrderSkuStatus { get; set; }
        public DCProduct DCProduct { get; set; }
        public OrderQuantityType OrderQuantityType { get; set; }
        public OrderTemplateProduct OrderTemplateProduct { get; set; }
    }
}
