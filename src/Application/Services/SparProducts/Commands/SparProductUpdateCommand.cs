namespace Engage.Application.Services.SparProducts.Commands;

public class SparProductUpdateCommand : IMapTo<SparProduct>, IRequest<SparProduct>
{
    public int Id { get; set; }
    public string ItemCode { get; init; }
    public string Name { get; init; }
    public float? UnitSize { get; init; }
    public int? SparUnitTypeId { get; init; }
    public string Barcode { get; init; }
    public int EngageBrandId { get; init; }
    public int SupplierId { get; init; }
    public int EngageSubCategoryId { get; init; }
    public int SparProductStatusId { get; init; }
    public int SparAnalysisGroupId { get; init; }
    public int SparSystemStatusId { get; init; }
    public int EvoLedgerId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SparProductUpdateCommand, SparProduct>();
    }
}

public record SparProductUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SparProductUpdateCommand, SparProduct>
{
    public async Task<SparProduct> Handle(SparProductUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.SparProducts.SingleOrDefaultAsync(e => e.SparProductId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateSparProductValidator : AbstractValidator<SparProductUpdateCommand>
{
    public UpdateSparProductValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ItemCode).NotEmpty().MaximumLength(30);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(200);
        RuleFor(e => e.UnitSize);
        RuleFor(e => e.SparUnitTypeId);
        RuleFor(e => e.Barcode).NotEmpty().MaximumLength(30);
        RuleFor(e => e.EngageBrandId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.SupplierId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.EngageSubCategoryId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.SparProductStatusId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.SparAnalysisGroupId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.SparSystemStatusId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.EvoLedgerId).NotEmpty().GreaterThan(0);
    }
}