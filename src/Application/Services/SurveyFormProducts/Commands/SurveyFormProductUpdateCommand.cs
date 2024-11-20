namespace Engage.Application.Services.SurveyFormProducts.Commands;

public class SurveyFormProductUpdateCommand : IMapTo<SurveyFormProduct>, IRequest<SurveyFormProduct>
{
    public int Id { get; set; }
    public int SurveyFormId { get; init; }
    public int EngageMasterProductId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyFormProductUpdateCommand, SurveyFormProduct>();
    }
}

public record SurveyFormProductUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormProductUpdateCommand, SurveyFormProduct>
{
    public async Task<SurveyFormProduct> Handle(SurveyFormProductUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.SurveyFormProducts.SingleOrDefaultAsync(e => e.SurveyFormProductId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateSurveyFormProductValidator : AbstractValidator<SurveyFormProductUpdateCommand>
{
    public UpdateSurveyFormProductValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.SurveyFormId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.EngageMasterProductId).NotEmpty().GreaterThan(0);
    }
}