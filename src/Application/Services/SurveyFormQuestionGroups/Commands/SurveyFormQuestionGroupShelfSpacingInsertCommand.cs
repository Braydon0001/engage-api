using Engage.Application.Services.SurveyFormQuestionGroupProducts.Commands;

namespace Engage.Application.Services.SurveyFormQuestionGroups.Commands;

public class SurveyFormQuestionGroupShelfSpacingInsertCommand : IMapTo<SurveyFormQuestionGroup>, IRequest<SurveyFormQuestionGroup>
{
    public string Name { get; init; }
    public string AvailableLabel { get; init; }
    public string OccupiedLabel { get; init; }
    public float Target { get; init; }
    //public int? DisplayOrder { get; init; }
    //public bool IsRequired { get; init; }
    //public List<JsonRule> Rules { get; init; }
    //public List<JsonSetting> Metadata { get; init; }
    public int SurveyFormId { get; init; }
    //public bool IsVirtualGroup { get; init; }
    //public float? CategoryTargetValue { get; init; }
    public List<int> EngageVariantProductIds { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyFormQuestionGroupShelfSpacingInsertCommand, SurveyFormQuestionGroup>();
    }
}

public record SurveyFormQuestionGroupShelfSpacingInsertHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator) : IRequestHandler<SurveyFormQuestionGroupShelfSpacingInsertCommand, SurveyFormQuestionGroup>
{
    public async Task<SurveyFormQuestionGroup> Handle(SurveyFormQuestionGroupShelfSpacingInsertCommand command, CancellationToken cancellationToken)
    {
        var groupDisplayOrder = await Context.SurveyFormQuestionGroups.Where(e => e.SurveyFormId == command.SurveyFormId).OrderByDescending(e => e.DisplayOrder).Select(e => e.DisplayOrder).FirstOrDefaultAsync(cancellationToken);

        var entity = Mapper.Map<SurveyFormQuestionGroupShelfSpacingInsertCommand, SurveyFormQuestionGroup>(command);

        entity.DisplayOrder = groupDisplayOrder == null ? 1 : groupDisplayOrder + 1;

        var meta = new JsonSetting() { Name = "Category Target", Type = "float", Value = command.Target.ToString() };

        entity.Metadata = [meta];

        Context.SurveyFormQuestionGroups.Add(entity);

        var opStatus = await Context.SaveChangesAsync(cancellationToken);

        if (opStatus.Status == true)
        {
            var surveyFormQuestionType = await Context.SurveyFormQuestionTypes.Where(e => e.Name == "Number").Select(e => e.SurveyFormQuestionTypeId).FirstOrDefaultAsync(cancellationToken);
            var availableQuestion = new SurveyFormQuestion()
            {
                SurveyFormQuestionGroupId = entity.SurveyFormQuestionGroupId,
                QuestionText = command.AvailableLabel,
                SurveyFormQuestionTypeId = surveyFormQuestionType,
                DisplayOrder = 1
            };
            Context.SurveyFormQuestions.Add(availableQuestion);
            var occupiedQuestion = new SurveyFormQuestion()
            {
                SurveyFormQuestionGroupId = entity.SurveyFormQuestionGroupId,
                QuestionText = command.OccupiedLabel,
                SurveyFormQuestionTypeId = surveyFormQuestionType,
                DisplayOrder = 2
            };
            Context.SurveyFormQuestions.Add(occupiedQuestion);
            await Mediator.Send(new SurveyFormQuestionGroupProductBatchUpdateCommand() { SurveyFormQuestionGroupId = entity.SurveyFormQuestionGroupId, EngageVariantProductIds = command.EngageVariantProductIds });
        }

        return entity;
    }
}

public class SurveyFormQuestionGroupShelfSpacingInsertValidator : AbstractValidator<SurveyFormQuestionGroupShelfSpacingInsertCommand>
{
    public SurveyFormQuestionGroupShelfSpacingInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty();
        RuleFor(e => e.AvailableLabel).NotEmpty();
        RuleFor(e => e.OccupiedLabel).NotEmpty();
        RuleFor(e => e.Target).NotEmpty().GreaterThan(0);
        //RuleFor(e => e.DisplayOrder);
        //RuleFor(e => e.IsRequired);
        //RuleFor(e => e.Rules);
        //RuleFor(e => e.Metadata);
        RuleFor(e => e.SurveyFormId).NotEmpty().GreaterThan(0);
        //RuleFor(e => e.IsVirtualGroup);
        //RuleFor(e => e.CategoryTargetValue);
    }
}