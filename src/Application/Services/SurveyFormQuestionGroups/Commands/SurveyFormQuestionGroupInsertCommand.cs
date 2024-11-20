using Engage.Application.Services.SurveyFormQuestionGroupProducts.Commands;

namespace Engage.Application.Services.SurveyFormQuestionGroups.Commands;

public class SurveyFormQuestionGroupInsertCommand : IMapTo<SurveyFormQuestionGroup>, IRequest<SurveyFormQuestionGroup>
{
    public string Name { get; init; }
    public int? DisplayOrder { get; init; }
    public bool IsRequired { get; init; }
    public int SurveyFormId { get; init; }
    public bool IsVirtualGroup { get; init; }
    public float? CategoryTargetValue { get; init; }
    public List<int> EngageVariantProductIds { get; init; }
    public List<JsonLink> Links { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyFormQuestionGroupInsertCommand, SurveyFormQuestionGroup>();
    }
}

public record SurveyFormQuestionGroupInsertHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator) : IRequestHandler<SurveyFormQuestionGroupInsertCommand, SurveyFormQuestionGroup>
{
    public async Task<SurveyFormQuestionGroup> Handle(SurveyFormQuestionGroupInsertCommand command, CancellationToken cancellationToken)
    {
        var groupDisplayOrder = await Context.SurveyFormQuestionGroups.Where(e => e.SurveyFormId == command.SurveyFormId).OrderByDescending(e => e.DisplayOrder).Select(e => e.DisplayOrder).FirstOrDefaultAsync(cancellationToken);

        var entity = Mapper.Map<SurveyFormQuestionGroupInsertCommand, SurveyFormQuestionGroup>(command);

        //if (entity != null && command.Links != null && command.Links.Count > 0)
        //{
        //    entity.Links = command.Links.Select(e => new JsonLink(e)).ToList();
        //}

        entity.DisplayOrder = groupDisplayOrder == null ? 1 : groupDisplayOrder + 1;

        Context.SurveyFormQuestionGroups.Add(entity);

        var opStatus = await Context.SaveChangesAsync(cancellationToken);

        if (opStatus.Status == true)
        {
            await Mediator.Send(new SurveyFormQuestionGroupProductBatchUpdateCommand() { SurveyFormQuestionGroupId = entity.SurveyFormQuestionGroupId, EngageVariantProductIds = command.EngageVariantProductIds }, cancellationToken);
        }

        return entity;
    }
}

public class SurveyFormQuestionGroupInsertValidator : AbstractValidator<SurveyFormQuestionGroupInsertCommand>
{
    public SurveyFormQuestionGroupInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty();
        RuleFor(e => e.DisplayOrder);
        RuleFor(e => e.IsRequired);
        RuleFor(e => e.SurveyFormId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.IsVirtualGroup);
        RuleFor(e => e.CategoryTargetValue);
    }
}