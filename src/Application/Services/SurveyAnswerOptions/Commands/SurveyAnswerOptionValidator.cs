namespace Engage.Application.Services.SurveyAnswerOptions.Commands;

public class SurveyAnswerOptionValidator<T> : AbstractValidator<T> where T : SurveyAnswerOptionCommand
{
    public SurveyAnswerOptionValidator()
    {
        RuleFor(x => x.SurveyAnswerId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.SurveyQuestionOptionId).GreaterThan(0).NotEmpty();
    }
}

public class CreateEmployeeSurveyOptionValidator : SurveyAnswerOptionValidator<CreateSurveyAnswerOptionCommand>
{
}

public class UpdateEmployeeSurveyOptionValidator : SurveyAnswerOptionValidator<UpdateSurveyAnswerOptionCommand>
{
    public UpdateEmployeeSurveyOptionValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
    }
}

public class BatchInsertEmployeeStoreSurveyOptionValidator : AbstractValidator<BatchCreateSurveyAnswerOptionCommand>
{
    public BatchInsertEmployeeStoreSurveyOptionValidator()
    {
        RuleFor(x => x.SurveyAnswerId).GreaterThan(0).NotEmpty();
    }
}
