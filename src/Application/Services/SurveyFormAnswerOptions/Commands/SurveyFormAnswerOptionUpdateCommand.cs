namespace Engage.Application.Services.SurveyFormAnswerOptions.Commands;

public class SurveyFormAnswerOptionUpdateCommand : IMapTo<SurveyFormAnswerOption>, IRequest<SurveyFormAnswerOption>
{
    public int Id { get; set; }
    public int SurveyFormAnswerId { get; init; }
    public int SurveyFormOptionId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyFormAnswerOptionUpdateCommand, SurveyFormAnswerOption>();
    }
}

public record SurveyFormAnswerOptionUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormAnswerOptionUpdateCommand, SurveyFormAnswerOption>
{
    public async Task<SurveyFormAnswerOption> Handle(SurveyFormAnswerOptionUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.SurveyFormAnswerOptions.SingleOrDefaultAsync(e => e.SurveyFormAnswerOptionId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateSurveyFormAnswerOptionValidator : AbstractValidator<SurveyFormAnswerOptionUpdateCommand>
{
    public UpdateSurveyFormAnswerOptionValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.SurveyFormAnswerId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.SurveyFormOptionId).NotEmpty().GreaterThan(0);
    }
}