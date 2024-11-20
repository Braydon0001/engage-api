namespace Engage.Domain.Entities;

public class Project : BaseAuditableEntity
{
    public Project()
    {
        ProjectUsers = new HashSet<ProjectUser>();
        ProjectTasks = new HashSet<ProjectTask>();
        ProjectTags = new HashSet<ProjectProjectTag>();
        ProjectNotes = new HashSet<ProjectNote>();
        ProjectAssignees = new HashSet<ProjectAssignee>();
        ProjectSuppliers = new HashSet<ProjectSupplier>();
        ProjectComments = new HashSet<ProjectComment>();
        EngageBrands = new HashSet<ProjectEngageBrand>();
    }
    public int ProjectId { get; set; }
    public string Name { get; set; }
    public List<JsonText> Note { get; set; }
    public int ProjectTypeId { get; set; }
    public int? ProjectSubTypeId { get; set; }
    public int ProjectStatusId { get; set; }
    public int ProjectPriorityId { get; set; }
    public int? EngageRegionId { get; set; }
    public int? ProjectCampaignId { get; set; }
    public int? ProjectCategoryId { get; set; }
    public int? ProjectSubCategoryId { get; set; }
    public int? OwnerId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public float? EstimatedHours { get; set; }
    public float? RemainingHours { get; set; }
    public DateTime? OpenedDate { get; set; }
    public string OpenedBy { get; set; }
    public DateTime? AssignedDate { get; set; }
    public string AssignedBy { get; set; }
    public DateTime? ClosedDate { get; set; }
    public string ClosedBy { get; set; }
    public List<string> Emails { get; set; }
    public List<JsonFile> Files { get; set; }

    // Navigation Properties

    public ProjectType ProjectType { get; set; }
    public ProjectSubType ProjectSubType { get; set; }
    public ProjectStatus ProjectStatus { get; set; }
    public ProjectPriority ProjectPriority { get; set; }
    public EngageRegion EngageRegion { get; set; }
    public ProjectCampaign ProjectCampaign { get; set; }
    public ProjectCategory ProjectCategory { get; set; }
    public ProjectSubCategory ProjectSubCategory { get; set; }
    public User Owner { get; set; }
    public ICollection<ProjectUser> ProjectUsers { get; private set; }
    public ICollection<ProjectTask> ProjectTasks { get; private set; }
    public ICollection<ProjectProjectTag> ProjectTags { get; private set; }
    public ICollection<ProjectNote> ProjectNotes { get; private set; }
    public ICollection<ProjectAssignee> ProjectAssignees { get; private set; }
    public ICollection<ProjectSupplier> ProjectSuppliers { get; private set; }
    public ICollection<ProjectComment> ProjectComments { get; private set; }
    public ICollection<ProjectEngageBrand> EngageBrands { get; private set; }
}