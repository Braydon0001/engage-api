namespace Engage.Domain.Entities;

public class ProductPrice : BaseAuditableEntity
{
    public int ProductPriceId { get; set; }
    public int ProductId { get; set; }
    public DateTime StartDate { get; set; }
    public decimal Price { get; set; }

    // Navigation Properties

    public Product Product { get; set; }
}