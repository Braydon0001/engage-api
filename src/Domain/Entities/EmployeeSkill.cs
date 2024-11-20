namespace Engage.Domain.Entities
{
    public class EmployeeSkill : BaseAuditableEntity
    {

        public int EmployeeSkillId { get; set; }
        public int EmployeeId { get; set; }
        public int SkillCategoryId { get; set; }
        public int ProficiencyId { get; set; }
        public int ExperienceId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Employee Employee { get; set; }
        public SkillCategory SkillCategory { get; set; }
        public Proficiency Proficiency { get; set; }
        public Experience Experience { get; set; }
        public List<JsonFile> Files { get; set; }
    }
}
