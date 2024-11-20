namespace Engage.Application.Services.SurveyFormAnswers.Commands;

public class SurveyFormAnswerInsertCommand : IMapTo<SurveyFormAnswer>, IRequest<SurveyFormAnswer>
{
    public string AnswerText { get; init; }
    public List<JsonSetting> Metadata { get; init; }
    public int SurveyFormSubmissionId { get; init; }
    public int SurveyFormQuestionId { get; init; }
    public int? SurveyFormReasonId { get; init; }
    public string AnswerUuid { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyFormAnswerInsertCommand, SurveyFormAnswer>();
    }
}

public record SurveyFormAnswerInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormAnswerInsertCommand, SurveyFormAnswer>
{
    public async Task<SurveyFormAnswer> Handle(SurveyFormAnswerInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<SurveyFormAnswerInsertCommand, SurveyFormAnswer>(command);

        Context.SurveyFormAnswers.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class SurveyFormAnswerInsertValidator : AbstractValidator<SurveyFormAnswerInsertCommand>
{
    public SurveyFormAnswerInsertValidator()
    {
        RuleFor(e => e.AnswerText).NotEmpty();
        RuleFor(e => e.Metadata);
        RuleFor(e => e.SurveyFormSubmissionId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.SurveyFormQuestionId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.SurveyFormReasonId);
    }
}