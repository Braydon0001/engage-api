namespace Engage.Domain.Entities.FileEntities;

public class EmployeeSkillFile : BaseFile
{
    public int EmployeeSkillFileId { get; set; }
    public int EmployeeSkillId { get; set; }

    // Navigation Properties
    public EmployeeSkill EmployeeSkill { get; set; }

}
