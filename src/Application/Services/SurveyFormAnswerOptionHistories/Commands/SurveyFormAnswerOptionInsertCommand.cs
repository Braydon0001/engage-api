namespace Engage.Application.Services.SurveyFormAnswerOptionHistories.Commands;

public class SurveyFormAnswerOptionHistoryInsertCommand : IMapTo<SurveyFormAnswerOptionHistory>, IRequest<SurveyFormAnswerOptionHistory>
{
    public int SurveyFormAnswerHistoryId { get; init; }
    public int? SurveyFormOptionId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyFormAnswerOptionHistoryInsertCommand, SurveyFormAnswerOptionHistory>();
    }
}

public record SurveyFormAnswerOptionHistoryInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormAnswerOptionHistoryInsertCommand, SurveyFormAnswerOptionHistory>
{
    public async Task<SurveyFormAnswerOptionHistory> Handle(SurveyFormAnswerOptionHistoryInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<SurveyFormAnswerOptionHistoryInsertCommand, SurveyFormAnswerOptionHistory>(command);

        Context.SurveyFormAnswerOptionHistories.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class SurveyFormAnswerOptionHistoryInsertValidator : AbstractValidator<SurveyFormAnswerOptionHistoryInsertCommand>
{
    public SurveyFormAnswerOptionHistoryInsertValidator()
    {
        RuleFor(e => e.SurveyFormAnswerHistoryId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.SurveyFormOptionId).NotEmpty();
    }
}