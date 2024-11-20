namespace Engage.Application.Services.EmployeeSkills.Models;

public class EmployeeSkillDto : IMapFrom<EmployeeSkill>
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public int SkillCategoryId { get; set; }
    public string SkillCategoryName { get; set; }
    public int ProficiencyId { get; set; }
    public string ProficiencyName { get; set; }
    public int ExperienceId { get; set; }
    public string ExperienceName { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool Disabled { get; set; }
    public List<JsonFile> Files { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeSkill, EmployeeSkillDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeSkillId))
            .ForMember(d => d.SkillCategoryName, opt => opt.MapFrom(s => s.SkillCategory.Name))
            .ForMember(d => d.ProficiencyName, opt => opt.MapFrom(s => s.Proficiency.Name))
            .ForMember(d => d.ExperienceName, opt => opt.MapFrom(s => s.Experience.Name));
    }
}
