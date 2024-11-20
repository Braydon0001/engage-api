namespace Engage.Domain.Entities;

public class OrderTemplate : BaseAuditableEntity
{
    public OrderTemplate()
    {
        OrderTemplateGroups = new HashSet<OrderTemplateGroup>();
    }

    public int OrderTemplateId { get; set; }
    public int OrderTemplateStatusId { get; set; }
    public int DistributionCenterId { get; set; }
    public string Name { get; set; }
    public string Note { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public List<JsonFile> Files { get; set; }

    // Navigation Properties

    public OrderTemplateStatus OrderTemplateStatus { get; set; }
    public DistributionCenter DistributionCenter { get; set; }
    public ICollection<OrderTemplateGroup> OrderTemplateGroups { get; set; }


}