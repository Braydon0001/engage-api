namespace Engage.Application.Services.SurveyFormQuestionOptions.Commands;

public class SurveyFormQuestionOptionUpdateCommand : IMapTo<SurveyFormQuestionOption>, IRequest<SurveyFormQuestionOption>
{
    public int Id { get; set; }
    public int SurveyFormQuestionId { get; init; }
    public int SurveyFormOptionId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyFormQuestionOptionUpdateCommand, SurveyFormQuestionOption>();
    }
}

public record SurveyFormQuestionOptionUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormQuestionOptionUpdateCommand, SurveyFormQuestionOption>
{
    public async Task<SurveyFormQuestionOption> Handle(SurveyFormQuestionOptionUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.SurveyFormQuestionOptions.SingleOrDefaultAsync(e => e.SurveyFormQuestionOptionId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateSurveyFormQuestionOptionValidator : AbstractValidator<SurveyFormQuestionOptionUpdateCommand>
{
    public UpdateSurveyFormQuestionOptionValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.SurveyFormQuestionId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.SurveyFormOptionId).NotEmpty().GreaterThan(0);
    }
}