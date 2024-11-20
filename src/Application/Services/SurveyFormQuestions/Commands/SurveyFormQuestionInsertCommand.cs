using Engage.Application.Services.SurveyFormQuestionOptions.Commands;
using Engage.Application.Services.SurveyFormQuestionProducts.Commands;
using Engage.Application.Services.SurveyFormQuestionReasons.Commands;

namespace Engage.Application.Services.SurveyFormQuestions.Commands;

public class SurveyFormQuestionInsertCommand : IMapTo<SurveyFormQuestion>, IRequest<SurveyFormQuestion>
{
    public string QuestionText { get; init; }
    public int? DisplayOrder { get; init; }
    public bool IsRequired { get; init; }
    public string Notes { get; init; }
    public int? SurveyFormQuestionGroupId { get; init; }
    public int SurveyFormQuestionTypeId { get; init; }
    public bool IsReasonRequired { get; init; }
    public bool? IsFalseReasonRequired { get; init; }
    public DateTime? MinDateTime { get; init; }
    public DateTime? MaxDateTime { get; init; }
    public List<int> EngageVariantProductIds { get; init; }
    public List<ReasonOption> AnswerReasons { get; init; }
    public List<ReasonOption> AnswerOptions { get; init; }
    public List<JsonLink> Links { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyFormQuestionInsertCommand, SurveyFormQuestion>();
    }
}

public record SurveyFormQuestionInsertHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator) : IRequestHandler<SurveyFormQuestionInsertCommand, SurveyFormQuestion>
{
    public async Task<SurveyFormQuestion> Handle(SurveyFormQuestionInsertCommand command, CancellationToken cancellationToken)
    {
        var displayOrder = await Context.SurveyFormQuestions.Where(e => e.SurveyFormQuestionGroupId == command.SurveyFormQuestionGroupId && !e.Deleted).OrderByDescending(e => e.DisplayOrder).Select(e => e.DisplayOrder).FirstOrDefaultAsync(cancellationToken);

        var entity = Mapper.Map<SurveyFormQuestionInsertCommand, SurveyFormQuestion>(command);

        //if (entity != null && command.Links != null && command.Links.Count > 0)
        //{
        //    entity.Links = command.Links.Select(e => new JsonLink(e)).ToList();
        //}

        entity.DisplayOrder = displayOrder == null ? 1 : displayOrder + 1;

        if (command.IsFalseReasonRequired != null)
        {
            entity.Metadata ??= [];
            var meta = new JsonSetting()
            {
                Name = "IsFalseReasonRequired",
                Type = "Flag",
                Value = command.IsFalseReasonRequired.Value.ToString(),
            };
            entity.Metadata.Add(meta);
        }

        Context.SurveyFormQuestions.Add(entity);

        var opStatus = await Context.SaveChangesAsync(cancellationToken);

        if (opStatus.Status == true)
        {
            await Mediator.Send(new SurveyFormQuestionProductBatchUpdateCommand() { SurveyFormQuestionId = entity.SurveyFormQuestionId, EngageVariantProductIds = command.EngageVariantProductIds }, cancellationToken);
            await Mediator.Send(new SurveyFormQuestionReasonBatchUpdateCommand() { SurveyFormQuestionId = entity.SurveyFormQuestionId, AnswerReasons = command.AnswerReasons }, cancellationToken);
            var type = await Context.SurveyFormQuestionTypes.Where(e => e.SurveyFormQuestionTypeId == entity.SurveyFormQuestionTypeId).Select(e => e.Name.ToLower()).FirstOrDefaultAsync(cancellationToken);
            if (type == "checkbox" || type == "radio")
            {
                await Mediator.Send(new SurveyFormQuestionOptionBatchUpdateCommand() { SurveyFormQuestionId = entity.SurveyFormQuestionId, AnswerOptions = command.AnswerOptions }, cancellationToken);
            }
        }

        return entity;
    }
}

public class SurveyFormQuestionInsertValidator : AbstractValidator<SurveyFormQuestionInsertCommand>
{
    public SurveyFormQuestionInsertValidator()
    {
        RuleFor(e => e.QuestionText).NotEmpty();
        RuleFor(e => e.DisplayOrder);
        RuleFor(e => e.IsRequired);
        RuleFor(e => e.Notes);
        RuleFor(e => e.SurveyFormQuestionGroupId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.SurveyFormQuestionTypeId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.IsReasonRequired);
        RuleFor(e => e.MinDateTime);
        RuleFor(e => e.MaxDateTime);
    }
}