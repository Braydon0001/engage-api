namespace Engage.Domain.Entities;

public class ProjectCampaign : BaseAuditableEntity
{
    public ProjectCampaign()
    {
        Projects = new HashSet<Project>();
    }
    public int ProjectCampaignId { get; set; }
    public string Name { get; set; }
    public int? EngageRegionId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public List<JsonText> Note { get; set; }
    public List<JsonFile> Files { get; set; }

    public EngageRegion EngageRegion { get; set; }
    public ICollection<Project> Projects { get; private set; }
}