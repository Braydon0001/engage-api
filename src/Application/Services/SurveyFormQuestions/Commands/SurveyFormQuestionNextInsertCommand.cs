using Engage.Application.Services.SurveyFormQuestionOptions.Commands;
using Engage.Application.Services.SurveyFormQuestionProducts.Commands;
using Engage.Application.Services.SurveyFormQuestionReasons.Commands;

namespace Engage.Application.Services.SurveyFormQuestions.Commands;

public class SurveyFormQuestionNextInsertCommand : IMapTo<SurveyFormQuestion>, IRequest<SurveyFormQuestion>
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
    public IFormFile[] SurveyFormQuestionFiles { get; init; }
    public List<ValueDto> Values { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyFormQuestionNextInsertCommand, SurveyFormQuestion>();
    }
}

public record SurveyFormQuestionNextInsertHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator, IFileService File) : IRequestHandler<SurveyFormQuestionNextInsertCommand, SurveyFormQuestion>
{
    public async Task<SurveyFormQuestion> Handle(SurveyFormQuestionNextInsertCommand command, CancellationToken cancellationToken)
    {
        var displayOrder = await Context.SurveyFormQuestions.Where(e => e.SurveyFormQuestionGroupId == command.SurveyFormQuestionGroupId && !e.Deleted).OrderByDescending(e => e.DisplayOrder).Select(e => e.DisplayOrder).FirstOrDefaultAsync(cancellationToken);
        var questionType = await Context.SurveyFormQuestionTypes.Where(e => e.SurveyFormQuestionTypeId == command.SurveyFormQuestionTypeId).Select(e => e.Name).FirstOrDefaultAsync(cancellationToken);
        var entity = Mapper.Map<SurveyFormQuestionNextInsertCommand, SurveyFormQuestion>(command);

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

        if (questionType == "Value Comparison" && command.Values != null)
        {
            var targetTypes = await Context.SurveyFormQuestionValueComparisonTargetTypes.ToListAsync(cancellationToken);
            var operations = await Context.SurveyFormQuestionValueComparisonOperations.ToListAsync(cancellationToken);
            entity.Metadata ??= [];
            var newMeta = new List<JsonSetting>(entity.Metadata);

            var values = new List<ValueDto>(command.Values);
            var valuesVm = new List<ValueVm>();

            foreach (var value in values)
            {
                var valueVm = new ValueVm()
                {
                    Label = value.Label,
                    HasTarget = value.HasTarget,
                    TargetType = value.TargetType.HasValue ? new OptionDto()
                    {
                        Id = value.TargetType.Value,
                        Name = targetTypes.Where(e => e.SurveyFormQuestionValueComparisonTargetTypeId == value.TargetType.Value)
                                          .Select(e => e.Name).FirstOrDefault()
                    } : null,
                    Reference = value.Reference.HasValue ? new OptionDto() { Id = value.Reference.Value, Name = "Value " + (value.Reference.Value + 1).ToString() } : null,
                    Operation = value.Operation.HasValue ? new OptionDto()
                    {
                        Id = value.Operation.Value,
                        Name = operations.Where(e => e.SurveyFormQuestionValueComparisonOperationId == value.Operation.Value)
                                          .Select(e => e.Name).FirstOrDefault()
                    } : null,
                    Target = value.Target,
                };
                valuesVm.Add(valueVm);
            }

            var newValues = new JsonSetting()
            {
                Name = "Values",
                Type = "Values",
                Value = JsonConvert.SerializeObject(valuesVm),
            };
            newMeta.Add(newValues);
            entity.Metadata = newMeta;
        }

        Context.SurveyFormQuestions.Add(entity);

        var opStatus = await Context.SaveChangesAsync(cancellationToken);

        if (command.SurveyFormQuestionFiles != null && command.SurveyFormQuestionFiles.Length != 0)
        {
            var fileUpdateCommand = new FileUpdateCommand
            {
                ContainerName = nameof(SurveyFormQuestion),
                EntityFiles = entity.Files,
                MaxFiles = 10,
                Files = command.SurveyFormQuestionFiles,
                Id = entity.SurveyFormQuestionId,
                FileType = "files",
            };

            entity.Files = await File.UpdateAsync(fileUpdateCommand, cancellationToken);
        }
        else
        {
            entity.Files = null;
        }

        if (command.Links == null || command.Links.Count == 0)
        {
            entity.Links = null;
        }

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

public class SurveyFormQuestionNextInsertValidator : AbstractValidator<SurveyFormQuestionNextInsertCommand>
{
    public SurveyFormQuestionNextInsertValidator()
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