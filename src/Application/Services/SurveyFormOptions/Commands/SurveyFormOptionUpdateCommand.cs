namespace Engage.Application.Services.SurveyFormOptions.Commands;

public class SurveyFormOptionUpdateCommand : IMapTo<SurveyFormOption>, IRequest<SurveyFormOption>
{
    public int Id { get; set; }
    public string Name { get; init; }
    public string Description { get; init; }
    public bool CompleteSurvey { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyFormOptionUpdateCommand, SurveyFormOption>();
    }
}

public record SurveyFormOptionUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormOptionUpdateCommand, SurveyFormOption>
{
    public async Task<SurveyFormOption> Handle(SurveyFormOptionUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.SurveyFormOptions.SingleOrDefaultAsync(e => e.SurveyFormOptionId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateSurveyFormOptionValidator : AbstractValidator<SurveyFormOptionUpdateCommand>
{
    public UpdateSurveyFormOptionValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
        RuleFor(e => e.Description).MaximumLength(1000);
    }
}