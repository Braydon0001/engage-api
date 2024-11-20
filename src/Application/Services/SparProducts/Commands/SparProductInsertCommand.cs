namespace Engage.Application.Services.SparProducts.Commands;

public class SparProductInsertCommand : IMapTo<SparProduct>, IRequest<SparProduct>
{
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
        profile.CreateMap<SparProductInsertCommand, SparProduct>();
    }
}

public record SparProductInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SparProductInsertCommand, SparProduct>
{
    public async Task<SparProduct> Handle(SparProductInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<SparProductInsertCommand, SparProduct>(command);
        
        Context.SparProducts.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class SparProductInsertValidator : AbstractValidator<SparProductInsertCommand>
{
    public SparProductInsertValidator()
    {
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