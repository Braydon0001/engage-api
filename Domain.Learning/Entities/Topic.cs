namespace Domain.Learning.Entities;

public class Topic
{
    public int TopicId { get; set; }
    public int? ApiTopicId { get; set; }
    public int? AccountId { get; set; }
    public string ModuleName { get; set; }
    public decimal ModulePassmark { get; set; }
    public string ModuleTags { get; set; }
    public bool ModuleTrainerDrivenOffsite { get; set; }
    public bool ModuleLearnerDriven { get; set; }
    public bool ModuleTrainerDrivenOnsite { get; set; }
    public bool ModuleIsActive { get; set; }
    public bool ModuleIsCoreModule { get; set; }
    public bool ModuleIsCriticalModule { get; set; }
    public bool ModuleIsDevelopmentModule { get; set; }
    public string TopicName { get; set; }
    public string ExternalCode { get; set; }
    public string ExternalModuleCode { get; set; }

    //public Account Account { get; set; }
}
