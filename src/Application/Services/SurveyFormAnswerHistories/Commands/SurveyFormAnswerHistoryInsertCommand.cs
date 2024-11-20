namespace Engage.Application.Services.SurveyFormAnswerHistories.Commands;

public class SurveyFormAnswerHistoryInsertCommand : IMapTo<SurveyFormAnswerHistory>, IRequest<OperationStatus>
{
    public int SurveyFormAnswerId { get; set; }
    public int? SurveyFormReasonId { get; set; }
    public string AnswerText { get; set; }
    public bool SaveChanges { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyFormAnswerHistoryInsertCommand, SurveyFormAnswerHistory>();
    }
}

public record SurveyFormAnswerHistoryInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormAnswerHistoryInsertCommand, OperationStatus>
{
    public async Task<OperationStatus> Handle(SurveyFormAnswerHistoryInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<SurveyFormAnswerHistoryInsertCommand, SurveyFormAnswerHistory>(command);

        Context.SurveyFormAnswerHistories.Add(entity);

        if (command.SaveChanges)
        {
            return await Context.SaveChangesAsync(cancellationToken);
        }


        return new OperationStatus { Status = true };
    }
}

public class SurveyFormAnswerHistoryInsertValidator : AbstractValidator<SurveyFormAnswerHistoryInsertCommand>
{
    public SurveyFormAnswerHistoryInsertValidator()
    {
        RuleFor(e => e.AnswerText).NotEmpty();
        RuleFor(e => e.SurveyFormAnswerId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.SurveyFormReasonId);
    }
}