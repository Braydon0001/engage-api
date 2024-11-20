namespace Engage.Application.Services.EmployeeQualifications.Commands;

public class EmployeeQualificationValidator<T> : AbstractValidator<T> where T : EmployeeQualificationCommand
{
    public EmployeeQualificationValidator()
    {
        RuleFor(x => x.EducationLevelId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.InstitutionTypeId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.Name).MaximumLength(120).NotEmpty();
        RuleFor(x => x.InstitutionName).MaximumLength(120);
        RuleFor(x => x.FinalYearSubjects).MaximumLength(250);
        RuleFor(x => x.Description).MaximumLength(120);
        RuleFor(x => x.CompletedDate).NotEmpty();
    }
}

public class CreateEmployeeQualificationValidator : EmployeeQualificationValidator<EmployeeQualificationCreateCommand>
{
    public CreateEmployeeQualificationValidator()
    {
        RuleFor(x => x.EmployeeId).GreaterThan(0).NotEmpty();
    }
}

public class UpdateEmployeeQualificationValidator : EmployeeQualificationValidator<EmployeeQualificationUpdateCommand>
{
    public UpdateEmployeeQualificationValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
    }
}
