namespace Engage.Application.Services.SurveyInstances.Commands;

public class SurveyInstanceValidator<T> : AbstractValidator<T> where T : SurveyInstanceCommand
{
    public SurveyInstanceValidator()
    {
        RuleFor(x => x.EmployeeId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.StoreId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.SurveyId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.Note).MaximumLength(300);
        RuleFor(x => x.SurveyDate).NotEmpty();
    }
}

public class CreateEmployeeSurveyValidator : SurveyInstanceValidator<CreateSurveyInstanceCommand>
{
}

public class UpdateEmployeeSurveyValidator : SurveyInstanceValidator<UpdateEmployeeStoreSurveyCommand>
{
    public UpdateEmployeeSurveyValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
    }
}
