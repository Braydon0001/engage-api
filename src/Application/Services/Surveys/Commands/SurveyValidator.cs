namespace Engage.Application.Services.Surveys.Commands;

public class SurveyValidator<T> : AbstractValidator<T> where T : SurveyCommand
{
    public SurveyValidator()
    {
        RuleFor(x => x.SurveyTypeId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.EngageSubGroupId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.SupplierId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.EngageBrandId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.Title).MaximumLength(220).NotEmpty();
        RuleFor(x => x.Note).MaximumLength(300);
        RuleFor(x => x.StartDate).NotEmpty();
        RuleFor(x => x.EndDate).GreaterThanOrEqualTo(x => x.StartDate);
        RuleForEach(x => x.EngageRegions).GreaterThan(0);
        RuleForEach(x => x.Stores).GreaterThan(0);
    }
}

public class CreateClaimValidator : SurveyValidator<CreateSurveyCommand>
{
    public CreateClaimValidator()
    {
    }
}

public class UpdateClaimValidator : SurveyValidator<UpdateSurveyCommand>
{
    public UpdateClaimValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
    }
}
