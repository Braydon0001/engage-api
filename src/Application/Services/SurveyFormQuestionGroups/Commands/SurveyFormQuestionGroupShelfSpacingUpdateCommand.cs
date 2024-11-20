using Engage.Application.Services.SurveyFormQuestionGroupProducts.Commands;

namespace Engage.Application.Services.SurveyFormQuestionGroups.Commands;

public class SurveyFormQuestionGroupShelfSpacingUpdateCommand : IMapTo<SurveyFormQuestionGroup>, IRequest<SurveyFormQuestionGroup>
{
    public int Id { get; set; }
    public string Name { get; init; }
    public string AvailableLabel { get; init; }
    public string OccupiedLabel { get; init; }
    public float Target { get; init; }
    public int SurveyFormId { get; init; }
    public List<int> EngageVariantProductIds { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyFormQuestionGroupShelfSpacingUpdateCommand, SurveyFormQuestionGroup>();
    }
}

public record SurveyFormQuestionGroupShelfSpacingUpdateHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator) : IRequestHandler<SurveyFormQuestionGroupShelfSpacingUpdateCommand, SurveyFormQuestionGroup>
{
    public async Task<SurveyFormQuestionGroup> Handle(SurveyFormQuestionGroupShelfSpacingUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.SurveyFormQuestionGroups.Include(e => e.SurveyFormQuestions).SingleOrDefaultAsync(e => e.SurveyFormQuestionGroupId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        if (entity.Metadata != null)
        {
            //set the metadata
            var meta = new List<JsonSetting>(entity.Metadata);

            var target = meta.Where(e => e.Name == "Category Target").FirstOrDefault();

            if (target == null)
            {
                var newTarget = new JsonSetting() { Name = "Category Target", Type = "float", Value = command.Target.ToString() };
                meta.Add(newTarget);
            }
            else
            {
                target.Value = command.Target.ToString();
            }

            entity.Metadata = meta;
        }
        else
        {
            //create the metadata
            var meta = new JsonSetting() { Name = "Category Target", Type = "float", Value = command.Target.ToString() };
            entity.Metadata = [meta];
        }

        var questions = entity.SurveyFormQuestions;

        if (questions == null || questions.Count == 0)
        {
            //create the questions
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
        }
        else
        {
            //update the questions
            var availableQuestion = entity.SurveyFormQuestions.ElementAt(0);
            availableQuestion.QuestionText = command.AvailableLabel;

            var occupiedQuestion = entity.SurveyFormQuestions.ElementAt(1);
            occupiedQuestion.QuestionText = command.OccupiedLabel;
        }

        await Mediator.Send(new SurveyFormQuestionGroupProductBatchUpdateCommand() { SurveyFormQuestionGroupId = entity.SurveyFormQuestionGroupId, EngageVariantProductIds = command.EngageVariantProductIds });

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class SurveyFormQuestionGroupShelfSpacingUpdateValidator : AbstractValidator<SurveyFormQuestionGroupShelfSpacingUpdateCommand>
{
    public SurveyFormQuestionGroupShelfSpacingUpdateValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty();
        RuleFor(e => e.AvailableLabel).NotEmpty();
        RuleFor(e => e.OccupiedLabel).NotEmpty();
        RuleFor(e => e.Target).NotEmpty().GreaterThan(0);
        RuleFor(e => e.SurveyFormId).NotEmpty().GreaterThanOrEqualTo(0);
    }
}