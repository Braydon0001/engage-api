namespace Engage.Application.Services.SurveyFormQuestionProducts.Commands;

public class SurveyFormQuestionProductUpdateCommand : IMapTo<SurveyFormQuestionProduct>, IRequest<SurveyFormQuestionProduct>
{
    public int Id { get; set; }
    public int SurveyFormQuestionId { get; init; }
    public int EngageVariantProductId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyFormQuestionProductUpdateCommand, SurveyFormQuestionProduct>();
    }
}

public record SurveyFormQuestionProductUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormQuestionProductUpdateCommand, SurveyFormQuestionProduct>
{
    public async Task<SurveyFormQuestionProduct> Handle(SurveyFormQuestionProductUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.SurveyFormQuestionProducts.SingleOrDefaultAsync(e => e.SurveyFormQuestionProductId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateSurveyFormQuestionProductValidator : AbstractValidator<SurveyFormQuestionProductUpdateCommand>
{
    public UpdateSurveyFormQuestionProductValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.SurveyFormQuestionId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.EngageVariantProductId).NotEmpty().GreaterThan(0);
    }
}