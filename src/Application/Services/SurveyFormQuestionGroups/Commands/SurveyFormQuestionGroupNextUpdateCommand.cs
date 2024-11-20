using Engage.Application.Services.SurveyFormQuestionGroupProducts.Commands;

namespace Engage.Application.Services.SurveyFormQuestionGroups.Commands;

public class SurveyFormQuestionGroupNextUpdateCommand : IMapTo<SurveyFormQuestionGroup>, IRequest<SurveyFormQuestionGroup>
{
    public int Id { get; set; }
    public string Name { get; init; }
    public int? DisplayOrder { get; set; }
    public bool IsRequired { get; init; }
    public int SurveyFormId { get; init; }
    public bool IsVirtualGroup { get; init; }
    public float? CategoryTargetValue { get; init; }
    public List<int> EngageVariantProductIds { get; init; }
    public List<JsonLink> Links { get; init; }
    public IFormFile[] SurveyFormQuestionGroupFiles { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyFormQuestionGroupNextUpdateCommand, SurveyFormQuestionGroup>();
    }
}

public record SurveyFormQuestionGroupNextUpdateHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator, IFileService File) : IRequestHandler<SurveyFormQuestionGroupNextUpdateCommand, SurveyFormQuestionGroup>
{
    public async Task<SurveyFormQuestionGroup> Handle(SurveyFormQuestionGroupNextUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.SurveyFormQuestionGroups.SingleOrDefaultAsync(e => e.SurveyFormQuestionGroupId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        command.DisplayOrder ??= entity.DisplayOrder;

        var mappedEntity = Mapper.Map(command, entity);

        if (mappedEntity != null && command.Links != null && command.Links.Count > 0)
        {
            mappedEntity.Links = command.Links;
        }
        else if (command.Links == null || command.Links.Count == 0)
        {
            mappedEntity.Links = null;
        }

        var fileUpdateCommand = new FileUpdateCommand
        {
            ContainerName = nameof(SurveyFormQuestionGroup),
            EntityFiles = mappedEntity.Files,
            MaxFiles = 10,
            Files = command.SurveyFormQuestionGroupFiles,
            Id = mappedEntity.SurveyFormQuestionGroupId,
            FileType = "files",
        };

        mappedEntity.Files = await File.UpdateAsync(fileUpdateCommand, cancellationToken);

        await Mediator.Send(new SurveyFormQuestionGroupProductBatchUpdateCommand() { SurveyFormQuestionGroupId = entity.SurveyFormQuestionGroupId, EngageVariantProductIds = command.EngageVariantProductIds }, cancellationToken);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class SurveyFormQuestionGroupNextUpdateValidator : AbstractValidator<SurveyFormQuestionGroupNextUpdateCommand>
{
    public SurveyFormQuestionGroupNextUpdateValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty();
        RuleFor(e => e.DisplayOrder);
        RuleFor(e => e.IsRequired);
        RuleFor(e => e.SurveyFormId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.IsVirtualGroup);
        RuleFor(e => e.CategoryTargetValue);
    }
}