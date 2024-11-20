namespace Engage.Domain.Entities;

public class OrderTemplateGroup : BaseAuditableEntity
{
    public OrderTemplateGroup()
    {
        OrderTemplateProducts = new HashSet<OrderTemplateProduct>();
    }

    public int OrderTemplateGroupId { get; set; }
    public int OrderTemplateId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Order { get; set; }

    // Navigation Properties
    public OrderTemplate OrderTemplate { get; set; }
    public ICollection<OrderTemplateProduct> OrderTemplateProducts { get; set; }
}
