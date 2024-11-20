namespace Engage.Application.Services.SurveyFormAnswerOptions.Commands;

public class SurveyFormAnswerOptionInsertCommand : IMapTo<SurveyFormAnswerOption>, IRequest<SurveyFormAnswerOption>
{
    public int SurveyFormAnswerId { get; init; }
    public int SurveyFormOptionId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyFormAnswerOptionInsertCommand, SurveyFormAnswerOption>();
    }
}

public record SurveyFormAnswerOptionInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormAnswerOptionInsertCommand, SurveyFormAnswerOption>
{
    public async Task<SurveyFormAnswerOption> Handle(SurveyFormAnswerOptionInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<SurveyFormAnswerOptionInsertCommand, SurveyFormAnswerOption>(command);
        
        Context.SurveyFormAnswerOptions.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class SurveyFormAnswerOptionInsertValidator : AbstractValidator<SurveyFormAnswerOptionInsertCommand>
{
    public SurveyFormAnswerOptionInsertValidator()
    {
        RuleFor(e => e.SurveyFormAnswerId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.SurveyFormOptionId).NotEmpty().GreaterThan(0);
    }
}