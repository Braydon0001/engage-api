namespace Engage.Application.Services.SurveyFormQuestionTypes.Commands;

public class SurveyFormQuestionTypeUpdateCommand : IMapTo<SurveyFormQuestionType>, IRequest<SurveyFormQuestionType>
{
    public int Id { get; set; }
    public string Name { get; init; }
    public string Description { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyFormQuestionTypeUpdateCommand, SurveyFormQuestionType>();
    }
}

public record SurveyFormQuestionTypeUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormQuestionTypeUpdateCommand, SurveyFormQuestionType>
{
    public async Task<SurveyFormQuestionType> Handle(SurveyFormQuestionTypeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.SurveyFormQuestionTypes.SingleOrDefaultAsync(e => e.SurveyFormQuestionTypeId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateSurveyFormQuestionTypeValidator : AbstractValidator<SurveyFormQuestionTypeUpdateCommand>
{
    public UpdateSurveyFormQuestionTypeValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
        RuleFor(e => e.Description).MaximumLength(1000);
    }
}