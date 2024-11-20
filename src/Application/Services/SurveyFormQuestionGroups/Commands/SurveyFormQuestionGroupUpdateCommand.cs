using Engage.Application.Services.SurveyFormQuestionGroupProducts.Commands;

namespace Engage.Application.Services.SurveyFormQuestionGroups.Commands;

public class SurveyFormQuestionGroupUpdateCommand : IMapTo<SurveyFormQuestionGroup>, IRequest<SurveyFormQuestionGroup>
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
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyFormQuestionGroupUpdateCommand, SurveyFormQuestionGroup>();
    }
}

public record SurveyFormQuestionGroupUpdateHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator) : IRequestHandler<SurveyFormQuestionGroupUpdateCommand, SurveyFormQuestionGroup>
{
    public async Task<SurveyFormQuestionGroup> Handle(SurveyFormQuestionGroupUpdateCommand command, CancellationToken cancellationToken)
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

        await Mediator.Send(new SurveyFormQuestionGroupProductBatchUpdateCommand() { SurveyFormQuestionGroupId = entity.SurveyFormQuestionGroupId, EngageVariantProductIds = command.EngageVariantProductIds }, cancellationToken);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateSurveyFormQuestionGroupValidator : AbstractValidator<SurveyFormQuestionGroupUpdateCommand>
{
    public UpdateSurveyFormQuestionGroupValidator()
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