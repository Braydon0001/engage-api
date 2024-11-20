namespace Engage.Domain.Entities;

public class OrderTemplateProduct : BaseAuditableEntity
{
    public OrderTemplateProduct()
    {
        OrderSkus = new HashSet<OrderSku>();
    }

    public int OrderTemplateProductId { get; set; }
    public int OrderTemplateGroupId { get; set; }
    public int DCProductId { get; set; }
    public int Order { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal PromotionPrice { get; set; }
    public decimal RecommendedPrice { get; set; }
    public decimal GrossProfitPercent { get; set; }
    public string Suffix { get; set; }
    public string Note { get; set; }
    public List<JsonFile> Files { get; set; }

    // Navigation Properties

    public OrderTemplateGroup OrderTemplateGroup { get; set; }
    public DCProduct DCProduct { get; set; }

    public ICollection<OrderSku> OrderSkus { get; set; }
}
