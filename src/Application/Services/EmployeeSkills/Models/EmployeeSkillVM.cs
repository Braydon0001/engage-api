namespace Engage.Application.Services.EmployeeSkills.Models;

public class EmployeeSkillVm : IMapFrom<EmployeeSkill>
{
    public int Id { get; set; }
    public OptionDto EmployeeId { get; set; }
    public OptionDto SkillCategoryId { get; set; }
    public OptionDto ProficiencyId { get; set; }
    public OptionDto ExperienceId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool Disabled { get; set; }
    public List<JsonFile> Files { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeSkill, EmployeeSkillVm>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeSkillId))
            .ForMember(d => d.EmployeeId, opt => opt.MapFrom(s => new OptionDto(s.EmployeeId, s.Employee.FirstName + " " + s.Employee.LastName)))
            .ForMember(d => d.SkillCategoryId, opt => opt.MapFrom(s => new OptionDto(s.SkillCategoryId, s.SkillCategory.Name)))
            .ForMember(d => d.ProficiencyId, opt => opt.MapFrom(s => new OptionDto(s.ProficiencyId, s.Proficiency.Name)))
            .ForMember(d => d.ExperienceId, opt => opt.MapFrom(s => new OptionDto(s.ExperienceId, s.Experience.Name)));
    }
}
