namespace Domain.Learning.Entities;

public class StaffLearningPath
{
    public int StaffLearningPathId { get; set; }
    public int? AccountId { get; set; }
    public int? StaffId { get; set; }
    public int? TopicId { get; set; }
    public int? AssessmentId { get; set; }
    public string StaffName { get; set; }
    public string ModuleName { get; set; }
    public string TopicName { get; set; }
    public string ControlSheetName { get; set; }
    public DateTime? ModuleStarted { get; set; }
    public DateTime? TopicStarted { get; set; }
    public DateTime? ControlSheetStarted { get; set; }
    public string AssessmentTrainerFullName { get; set; }
    public decimal AssessmentScore { get; set; }
    public bool IsModuleCompleted { get; set; }
    public bool IsTopicCompleted { get; set; }
    public bool IsControlSheetCompleted { get; set; }
    public int? NoOfTopicsCompleted { get; set; }
    public DateTime? DateOfTopicCompletion { get; set; }
    public string ExternalStaffCode { get; set; }
    public string ExternalTopicCode { get; set; }

    //public Account Account { get; set; }
    public Staff Staff { get; set; }
    public Topic Topic { get; set; }
    //public Assessment Assessment { get; set; }
}
