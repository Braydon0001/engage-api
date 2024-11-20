namespace Engage.Application.Services.SurveyFormQuestionProducts.Commands;

public class SurveyFormQuestionProductInsertCommand : IMapTo<SurveyFormQuestionProduct>, IRequest<SurveyFormQuestionProduct>
{
    public int SurveyFormQuestionId { get; init; }
    public int EngageVariantProductId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyFormQuestionProductInsertCommand, SurveyFormQuestionProduct>();
    }
}

public record SurveyFormQuestionProductInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormQuestionProductInsertCommand, SurveyFormQuestionProduct>
{
    public async Task<SurveyFormQuestionProduct> Handle(SurveyFormQuestionProductInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<SurveyFormQuestionProductInsertCommand, SurveyFormQuestionProduct>(command);
        
        Context.SurveyFormQuestionProducts.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class SurveyFormQuestionProductInsertValidator : AbstractValidator<SurveyFormQuestionProductInsertCommand>
{
    public SurveyFormQuestionProductInsertValidator()
    {
        RuleFor(e => e.SurveyFormQuestionId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.EngageVariantProductId).NotEmpty().GreaterThan(0);
    }
}