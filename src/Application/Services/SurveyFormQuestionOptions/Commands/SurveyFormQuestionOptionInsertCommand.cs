namespace Engage.Application.Services.SurveyFormQuestionOptions.Commands;

public class SurveyFormQuestionOptionInsertCommand : IMapTo<SurveyFormQuestionOption>, IRequest<SurveyFormQuestionOption>
{
    public int SurveyFormQuestionId { get; init; }
    public int SurveyFormOptionId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyFormQuestionOptionInsertCommand, SurveyFormQuestionOption>();
    }
}

public record SurveyFormQuestionOptionInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormQuestionOptionInsertCommand, SurveyFormQuestionOption>
{
    public async Task<SurveyFormQuestionOption> Handle(SurveyFormQuestionOptionInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<SurveyFormQuestionOptionInsertCommand, SurveyFormQuestionOption>(command);
        
        Context.SurveyFormQuestionOptions.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class SurveyFormQuestionOptionInsertValidator : AbstractValidator<SurveyFormQuestionOptionInsertCommand>
{
    public SurveyFormQuestionOptionInsertValidator()
    {
        RuleFor(e => e.SurveyFormQuestionId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.SurveyFormOptionId).NotEmpty().GreaterThan(0);
    }
}