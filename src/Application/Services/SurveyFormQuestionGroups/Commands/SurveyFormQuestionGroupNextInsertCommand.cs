using Engage.Application.Services.SurveyFormQuestionGroupProducts.Commands;

namespace Engage.Application.Services.SurveyFormQuestionGroups.Commands;

public class SurveyFormQuestionGroupNextInsertCommand : IMapTo<SurveyFormQuestionGroup>, IRequest<SurveyFormQuestionGroup>
{
    public string Name { get; init; }
    public int? DisplayOrder { get; init; }
    public bool IsRequired { get; init; }
    public int SurveyFormId { get; init; }
    public bool IsVirtualGroup { get; init; }
    public float? CategoryTargetValue { get; init; }
    public List<int> EngageVariantProductIds { get; init; }
    public IFormFile[] SurveyFormQuestionGroupFiles { get; init; }
    public List<JsonLink> Links { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyFormQuestionGroupNextInsertCommand, SurveyFormQuestionGroup>();
    }
}

public record SurveyFormQuestionGroupNextInsertHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator, IFileService File) : IRequestHandler<SurveyFormQuestionGroupNextInsertCommand, SurveyFormQuestionGroup>
{
    public async Task<SurveyFormQuestionGroup> Handle(SurveyFormQuestionGroupNextInsertCommand command, CancellationToken cancellationToken)
    {
        var groupDisplayOrder = await Context.SurveyFormQuestionGroups.Where(e => e.SurveyFormId == command.SurveyFormId).OrderByDescending(e => e.DisplayOrder).Select(e => e.DisplayOrder).FirstOrDefaultAsync(cancellationToken);

        var entity = Mapper.Map<SurveyFormQuestionGroupNextInsertCommand, SurveyFormQuestionGroup>(command);

        entity.DisplayOrder = groupDisplayOrder == null ? 1 : groupDisplayOrder + 1;

        Context.SurveyFormQuestionGroups.Add(entity);

        var opStatus = await Context.SaveChangesAsync(cancellationToken);

        var fileUpdateCommand = new FileUpdateCommand
        {
            ContainerName = nameof(SurveyFormQuestionGroup),
            EntityFiles = entity.Files,
            MaxFiles = 10,
            Files = command.SurveyFormQuestionGroupFiles,
            Id = entity.SurveyFormQuestionGroupId,
            FileType = "files",
        };

        entity.Files = await File.UpdateAsync(fileUpdateCommand, cancellationToken);

        if (opStatus.Status == true)
        {
            await Mediator.Send(new SurveyFormQuestionGroupProductBatchUpdateCommand() { SurveyFormQuestionGroupId = entity.SurveyFormQuestionGroupId, EngageVariantProductIds = command.EngageVariantProductIds }, cancellationToken);
        }

        return entity;
    }
}

public class SurveyFormQuestionGroupNextInsertValidator : AbstractValidator<SurveyFormQuestionGroupNextInsertCommand>
{
    public SurveyFormQuestionGroupNextInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty();
        RuleFor(e => e.DisplayOrder);
        RuleFor(e => e.IsRequired);
        RuleFor(e => e.SurveyFormId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.IsVirtualGroup);
        RuleFor(e => e.CategoryTargetValue);
    }
}