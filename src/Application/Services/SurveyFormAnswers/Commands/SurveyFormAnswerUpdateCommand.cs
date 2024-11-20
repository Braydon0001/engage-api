namespace Engage.Application.Services.SurveyFormAnswers.Commands;

public class SurveyFormAnswerUpdateCommand : IMapTo<SurveyFormAnswer>, IRequest<SurveyFormAnswer>
{
    public int? Id { get; set; }
    public string AnswerText { get; init; }
    public List<JsonSetting> Metadata { get; init; }
    public int SurveyFormSubmissionId { get; init; }
    public int SurveyFormQuestionId { get; init; }
    public int? SurveyFormReasonId { get; init; }
    public string AnswerUuid { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyFormAnswerUpdateCommand, SurveyFormAnswer>();
    }
}

public record SurveyFormAnswerUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormAnswerUpdateCommand, SurveyFormAnswer>
{
    public async Task<SurveyFormAnswer> Handle(SurveyFormAnswerUpdateCommand command, CancellationToken cancellationToken)
    {
        SurveyFormAnswer entity;

        if (command.Id == null)
        {
            entity = await Context.SurveyFormAnswers.SingleOrDefaultAsync(e => e.AnswerUuid == command.AnswerUuid, cancellationToken);
        }
        else
        {
            entity = await Context.SurveyFormAnswers.SingleOrDefaultAsync(e => e.SurveyFormAnswerId == command.Id, cancellationToken);
        }

        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateSurveyFormAnswerValidator : AbstractValidator<SurveyFormAnswerUpdateCommand>
{
    public UpdateSurveyFormAnswerValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.AnswerText).NotEmpty();
        RuleFor(e => e.Metadata);
        RuleFor(e => e.SurveyFormSubmissionId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.SurveyFormQuestionId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.SurveyFormReasonId);
    }
}