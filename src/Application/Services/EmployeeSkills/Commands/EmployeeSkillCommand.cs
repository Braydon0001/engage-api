namespace Engage.Application.Services.EmployeeSkills.Commands;

public class EmployeeSkillCommand : IMapTo<EmployeeSkill>
{
    public int SkillCategoryId { get; set; }
    public int ProficiencyId { get; set; }
    public int ExperienceId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool Disabled { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeSkillCommand, EmployeeSkill>();
    }
}
