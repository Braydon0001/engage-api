namespace Engage.Domain.Entities;

public class ProjectTask : BaseAuditableEntity
{
    public ProjectTask()
    {
        ProjectTaskNotes = new HashSet<ProjectTaskNote>();
        ProjectTaskProjectStakeholderUsers = new HashSet<ProjectTaskProjectStakeholderUser>();
    }
    public int ProjectTaskId { get; set; }
    public string Name { get; set; }
    public string Note { get; set; }
    public int ProjectId { get; set; }
    public int? ProjectTaskTypeId { get; set; }
    public int ProjectTaskStatusId { get; set; }
    public int? ProjectTaskPriorityId { get; set; }
    public int? ProjectTaskSeverityId { get; set; }
    public int? UserId { get; set; }
    public int? ProjectStakeholderId { get; set; }
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
    public bool IsClosed { get; set; }
    public List<JsonFile> Files { get; set; }

    // Navigation Properties

    public Project Project { get; set; }
    public ProjectTaskType ProjectTaskType { get; set; }
    public ProjectTaskStatus ProjectTaskStatus { get; set; }
    public ProjectTaskPriority ProjectTaskPriority { get; set; }
    public ProjectTaskSeverity ProjectTaskSeverity { get; set; }
    public User User { get; set; }
    public ProjectStakeholderUser ProjectStakeholder { get; set; }
    public ICollection<ProjectTaskNote> ProjectTaskNotes { get; private set; }
    public ICollection<ProjectTaskProjectStakeholderUser> ProjectTaskProjectStakeholderUsers { get; private set; }
}