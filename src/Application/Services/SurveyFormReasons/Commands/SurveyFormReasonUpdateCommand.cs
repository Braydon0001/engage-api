namespace Engage.Application.Services.SurveyFormReasons.Commands;

public class SurveyFormReasonUpdateCommand : IMapTo<SurveyFormReason>, IRequest<SurveyFormReason>
{
    public int Id { get; set; }
    public string Name { get; init; }
    public string Description { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyFormReasonUpdateCommand, SurveyFormReason>();
    }
}

public record SurveyFormReasonUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormReasonUpdateCommand, SurveyFormReason>
{
    public async Task<SurveyFormReason> Handle(SurveyFormReasonUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.SurveyFormReasons.SingleOrDefaultAsync(e => e.SurveyFormReasonId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateSurveyFormReasonValidator : AbstractValidator<SurveyFormReasonUpdateCommand>
{
    public UpdateSurveyFormReasonValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
        RuleFor(e => e.Description).MaximumLength(1000);
    }
}