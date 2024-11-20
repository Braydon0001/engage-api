namespace Engage.Application.Services.EmployeeSkills.Commands;

public class EmployeeSkillValidator<T> : AbstractValidator<T> where T : EmployeeSkillCommand
{
    public EmployeeSkillValidator()
    {
        RuleFor(x => x.SkillCategoryId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.ProficiencyId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.ExperienceId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.Name).MaximumLength(120).NotEmpty();
        RuleFor(x => x.Description).MaximumLength(120);
    }
}

public class CreateEmployeeSkillValidator : EmployeeSkillValidator<CreateEmployeeSkillCommand>
{
    public CreateEmployeeSkillValidator()
    {
        RuleFor(x => x.EmployeeId).GreaterThan(0).NotEmpty();
    }
}

public class UpdateEmployeeSkillValidator : EmployeeSkillValidator<UpdateEmployeeSkillCommand>
{
    public UpdateEmployeeSkillValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
    }
}
