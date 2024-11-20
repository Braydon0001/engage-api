namespace Engage.Application.Services.SurveyFormQuestionReasons.Commands;

public class SurveyFormQuestionReasonInsertCommand : IMapTo<SurveyFormQuestionReason>, IRequest<SurveyFormQuestionReason>
{
    public int SurveyFormQuestionId { get; init; }
    public int SurveyFormReasonId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyFormQuestionReasonInsertCommand, SurveyFormQuestionReason>();
    }
}

public record SurveyFormQuestionReasonInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormQuestionReasonInsertCommand, SurveyFormQuestionReason>
{
    public async Task<SurveyFormQuestionReason> Handle(SurveyFormQuestionReasonInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<SurveyFormQuestionReasonInsertCommand, SurveyFormQuestionReason>(command);
        
        Context.SurveyFormQuestionReasons.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class SurveyFormQuestionReasonInsertValidator : AbstractValidator<SurveyFormQuestionReasonInsertCommand>
{
    public SurveyFormQuestionReasonInsertValidator()
    {
        RuleFor(e => e.SurveyFormQuestionId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.SurveyFormReasonId).NotEmpty().GreaterThan(0);
    }
}