namespace Engage.Application.Services.SurveyFormReasons.Commands;

public class SurveyFormReasonInsertCommand : IMapTo<SurveyFormReason>, IRequest<SurveyFormReason>
{
    public string Name { get; init; }
    public string Description { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyFormReasonInsertCommand, SurveyFormReason>();
    }
}

public record SurveyFormReasonInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormReasonInsertCommand, SurveyFormReason>
{
    public async Task<SurveyFormReason> Handle(SurveyFormReasonInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<SurveyFormReasonInsertCommand, SurveyFormReason>(command);
        
        Context.SurveyFormReasons.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class SurveyFormReasonInsertValidator : AbstractValidator<SurveyFormReasonInsertCommand>
{
    public SurveyFormReasonInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
        RuleFor(e => e.Description).MaximumLength(1000);
    }
}