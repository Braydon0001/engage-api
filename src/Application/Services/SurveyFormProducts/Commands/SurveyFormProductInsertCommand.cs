namespace Engage.Application.Services.SurveyFormProducts.Commands;

public class SurveyFormProductInsertCommand : IMapTo<SurveyFormProduct>, IRequest<SurveyFormProduct>
{
    public int SurveyFormId { get; init; }
    public int EngageMasterProductId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyFormProductInsertCommand, SurveyFormProduct>();
    }
}

public record SurveyFormProductInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormProductInsertCommand, SurveyFormProduct>
{
    public async Task<SurveyFormProduct> Handle(SurveyFormProductInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<SurveyFormProductInsertCommand, SurveyFormProduct>(command);
        
        Context.SurveyFormProducts.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class SurveyFormProductInsertValidator : AbstractValidator<SurveyFormProductInsertCommand>
{
    public SurveyFormProductInsertValidator()
    {
        RuleFor(e => e.SurveyFormId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.EngageMasterProductId).NotEmpty().GreaterThan(0);
    }
}