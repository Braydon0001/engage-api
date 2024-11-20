namespace Engage.Application.Services.SurveyFormQuestionTypes.Commands;

public class SurveyFormQuestionTypeInsertCommand : IMapTo<SurveyFormQuestionType>, IRequest<SurveyFormQuestionType>
{
    public string Name { get; init; }
    public string Description { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyFormQuestionTypeInsertCommand, SurveyFormQuestionType>();
    }
}

public record SurveyFormQuestionTypeInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormQuestionTypeInsertCommand, SurveyFormQuestionType>
{
    public async Task<SurveyFormQuestionType> Handle(SurveyFormQuestionTypeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<SurveyFormQuestionTypeInsertCommand, SurveyFormQuestionType>(command);
        
        Context.SurveyFormQuestionTypes.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class SurveyFormQuestionTypeInsertValidator : AbstractValidator<SurveyFormQuestionTypeInsertCommand>
{
    public SurveyFormQuestionTypeInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
        RuleFor(e => e.Description).MaximumLength(1000);
    }
}