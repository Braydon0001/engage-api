namespace Engage.Application.Services.SurveyStores;

public class CreateSurveyStoresValidator : AbstractValidator<CreateSurveyStoresCommand>
{
    public CreateSurveyStoresValidator()
    {
        RuleFor(x => x.SurveyId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.Stores).NotEmpty();
        RuleForEach(x => x.Stores).GreaterThan(0);
    }
}

public class CreateSurveyStoresWithCriteriaValidator : AbstractValidator<CreateSurveyStoresWithCriteriaCommand>
{
    public CreateSurveyStoresWithCriteriaValidator()
    {
        RuleFor(x => x.SurveyId).GreaterThan(0).NotEmpty();
        RuleForEach(x => x.EngageRegions).GreaterThan(0);
        RuleForEach(x => x.StoreClusters).GreaterThan(0);
        RuleForEach(x => x.StoreFormats).GreaterThan(0);
        RuleForEach(x => x.StoreLSMs).GreaterThan(0);
        RuleForEach(x => x.StoreTypes).GreaterThan(0);
    }
}

public class RemoveSurveyStoreValidator : AbstractValidator<DeleteSurveyStoreCommand>
{
    public RemoveSurveyStoreValidator()
    {
        RuleFor(x => x.SurveyId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.StoreId).GreaterThan(0).NotEmpty();
    }
}
