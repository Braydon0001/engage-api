namespace Engage.Application.Services.SurveyQuestions.Commands;

public class SurveyQuestionValidator<T> : AbstractValidator<T> where T : SurveyQuestionCommand
{
    public SurveyQuestionValidator()
    {
        RuleFor(x => x.QuestionTypeId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.Question).MaximumLength(300).NotEmpty();
        RuleForEach(x => x.QuestionFalseReasonIds).GreaterThan(0);
    }
}

public class CreateSurveyQuestionValidator : SurveyQuestionValidator<CreateSurveyQuestionCommand>
{
    public CreateSurveyQuestionValidator()
    {
        RuleFor(x => x.SurveyId).GreaterThan(0).NotEmpty();
    }
}

public class UpdateSurveyQuestionValidator : SurveyQuestionValidator<UpdateSurveyQuestionCommand>
{
    public UpdateSurveyQuestionValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
        RuleFor(x => x.Option1Id).GreaterThan(0);
        RuleFor(x => x.Option2Id).GreaterThan(0);
        RuleFor(x => x.Option3Id).GreaterThan(0);
        RuleFor(x => x.Option4Id).GreaterThan(0);
        RuleFor(x => x.Option5Id).GreaterThan(0);
        RuleFor(x => x.Option6Id).GreaterThan(0);
        RuleFor(x => x.Option7Id).GreaterThan(0);
        RuleFor(x => x.Option8Id).GreaterThan(0);
        RuleFor(x => x.Option9Id).GreaterThan(0);
        RuleFor(x => x.Option10Id).GreaterThan(0);
    }
}
