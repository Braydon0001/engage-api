using Engage.Application.Services.SurveyFormProducts.Commands;

namespace Engage.Application.Services.SurveyForms.Commands;

public class SurveyFormUpdateCommand : IMapTo<SurveyForm>, IRequest<SurveyForm>
{
    public int Id { get; set; }
    public string Title { get; init; }
    public string Description { get; init; }
    public string Note { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public bool IsRequired { get; init; }
    public bool IsRecurring { get; init; }
    public bool IsDisabled { get; init; }
    public int SurveyFormTypeId { get; init; }
    public int? EngageSubgroupId { get; init; }
    public int? SupplierId { get; init; }
    public int? EngageBrandId { get; init; }
    public bool IsStoreRecurring { get; init; }
    public bool IsEmployeeSurvey { get; init; }
    public bool IgnoreSubgroup { get; init; }
    public List<int> EngageMasterProductIds { get; init; }
    public bool IsTemplate { get; init; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyFormUpdateCommand, SurveyForm>();
    }
}

public record SurveyFormUpdateHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator) : IRequestHandler<SurveyFormUpdateCommand, SurveyForm>
{
    public async Task<SurveyForm> Handle(SurveyFormUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.SurveyForms.SingleOrDefaultAsync(e => e.SurveyFormId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        await Mediator.Send(new SurveyFormProductBatchUpdateCommand() { SurveyFormId = command.Id, EngageMasterProductIds = command.EngageMasterProductIds }, cancellationToken);

        return mappedEntity;
    }
}

public class UpdateSurveyFormValidator : AbstractValidator<SurveyFormUpdateCommand>
{
    public UpdateSurveyFormValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Title).NotEmpty();
        RuleFor(e => e.Description);
        RuleFor(e => e.Note);
        RuleFor(e => e.StartDate);
        RuleFor(e => e.EndDate);
        RuleFor(e => e.IsRequired);
        RuleFor(e => e.IsRecurring);
        RuleFor(e => e.IsDisabled);
        RuleFor(e => e.SurveyFormTypeId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.EngageSubgroupId).NotEmpty().GreaterThan(0).When(e => e.SurveyFormTypeId == 1);
        RuleFor(e => e.SupplierId).NotEmpty().GreaterThan(0).When(e => e.SurveyFormTypeId == 1);
        RuleFor(e => e.EngageBrandId).NotEmpty().GreaterThan(0).When(e => e.SurveyFormTypeId == 1);
        RuleFor(e => e.IsStoreRecurring);
        RuleFor(e => e.IsEmployeeSurvey);
    }
}