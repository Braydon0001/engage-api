namespace Engage.Application.Services.SurveyFormOptions.Commands;

public class SurveyFormOptionInsertCommand : IMapTo<SurveyFormOption>, IRequest<SurveyFormOption>
{
    public string Name { get; init; }
    public string Description { get; init; }
    public bool CompleteSurvey { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyFormOptionInsertCommand, SurveyFormOption>();
    }
}

public record SurveyFormOptionInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormOptionInsertCommand, SurveyFormOption>
{
    public async Task<SurveyFormOption> Handle(SurveyFormOptionInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<SurveyFormOptionInsertCommand, SurveyFormOption>(command);

        Context.SurveyFormOptions.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class SurveyFormOptionInsertValidator : AbstractValidator<SurveyFormOptionInsertCommand>
{
    public SurveyFormOptionInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
        RuleFor(e => e.Description).MaximumLength(1000);
    }
}