namespace Engage.Application.Services.SurveyFormQuestionReasons.Commands;

public class SurveyFormQuestionReasonUpdateCommand : IMapTo<SurveyFormQuestionReason>, IRequest<SurveyFormQuestionReason>
{
    public int Id { get; set; }
    public int SurveyFormQuestionId { get; init; }
    public int SurveyFormReasonId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyFormQuestionReasonUpdateCommand, SurveyFormQuestionReason>();
    }
}

public record SurveyFormQuestionReasonUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormQuestionReasonUpdateCommand, SurveyFormQuestionReason>
{
    public async Task<SurveyFormQuestionReason> Handle(SurveyFormQuestionReasonUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.SurveyFormQuestionReasons.SingleOrDefaultAsync(e => e.SurveyFormQuestionReasonId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateSurveyFormQuestionReasonValidator : AbstractValidator<SurveyFormQuestionReasonUpdateCommand>
{
    public UpdateSurveyFormQuestionReasonValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.SurveyFormQuestionId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.SurveyFormReasonId).NotEmpty().GreaterThan(0);
    }
}