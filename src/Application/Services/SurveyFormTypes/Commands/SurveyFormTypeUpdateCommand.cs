namespace Engage.Application.Services.SurveyFormTypes.Commands;

public class SurveyFormTypeUpdateCommand : IMapTo<SurveyFormType>, IRequest<SurveyFormType>
{
    public int Id { get; set; }
    public string Name { get; init; }
    public string Description { get; init; }
    public bool HideEmployeeTargeting { get; init; }
    public bool HideEngageSupplier { get; init; }
    public bool HideEndDate { get; init; }
    public bool HideRecurring { get; set; }
    public bool HideStoreRecurring { get; set; }
    public bool HideSurveyRequired { get; set; }
    public bool HideAddQuestionGroup { get; set; }
    public bool HideAddQuestion { get; set; }
    public bool HideDeleteQuestion { get; set; }
    public bool HideDisableGroup { get; set; }
    public bool HideDisableQuestion { get; set; }
    public bool HideReorderGroup { get; set; }
    public bool HideReorderQuestion { get; set; }
    public bool UseTemplate { get; set; }
    public int? SurveyFormTemplateId { get; set; }
    public bool HideStoreTargeting { get; set; }
    public bool HideGroupRules { get; set; }
    public bool HideQuestionRules { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyFormTypeUpdateCommand, SurveyFormType>();
    }
}

public record SurveyFormTypeUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormTypeUpdateCommand, SurveyFormType>
{
    public async Task<SurveyFormType> Handle(SurveyFormTypeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.SurveyFormTypes.SingleOrDefaultAsync(e => e.SurveyFormTypeId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateSurveyFormTypeValidator : AbstractValidator<SurveyFormTypeUpdateCommand>
{
    public UpdateSurveyFormTypeValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
        RuleFor(e => e.Description).MaximumLength(1000);
        RuleFor(e => e.HideEmployeeTargeting);
        RuleFor(e => e.HideEngageSupplier);
        RuleFor(e => e.HideEndDate);
        RuleFor(e => e.HideAddQuestionGroup);
        RuleFor(e => e.HideAddQuestion);
        RuleFor(e => e.HideRecurring);
        RuleFor(e => e.HideReorderGroup);
        RuleFor(e => e.HideSurveyRequired);
    }
}