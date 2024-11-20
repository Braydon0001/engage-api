namespace Engage.Application.Services.SurveyQuestionOptions.Commands;

public class SurveyQuestionOptionValidator<T> : AbstractValidator<T> where T : SurveyQuestionOptionCommand
{
    public SurveyQuestionOptionValidator()
    {

    }
}

public class CreateClaimValidator : SurveyQuestionOptionValidator<CreateSurveyQuestionOptionCommand>
{
    public CreateClaimValidator()
    {
        RuleFor(x => x.SurveyQuestionId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.Option).MaximumLength(300).NotEmpty();
    }
}

public class UpdateClaimValidator : SurveyQuestionOptionValidator<UpdateSurveyQuestionOptionCommand>
{
    public UpdateClaimValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
    }
}
