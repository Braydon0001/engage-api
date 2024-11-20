namespace Engage.Application.Services.SurveyAnswers.Commands;

public class SurveyAnswerValidator<T> : AbstractValidator<T> where T : SurveyAnswerCommand
{
    public SurveyAnswerValidator()
    {
        RuleFor(x => x.QuestionFalseReasonId).GreaterThan(0);
        RuleFor(x => x.Answer).MaximumLength(1000);
    }
}

public class CreateEmployeeSurveyAnswerValidator : SurveyAnswerValidator<CreateSurveyAnswerCommand>
{
    public CreateEmployeeSurveyAnswerValidator()
    {
        RuleFor(x => x.SurveyInstanceId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.SurveyQuestionId).GreaterThan(0).NotEmpty();
    }

}

public class UpdateEmployeeSurveyAnswerValidator : SurveyAnswerValidator<UpdateSurveyAnswerCommand>
{
    public UpdateEmployeeSurveyAnswerValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
    }
}
