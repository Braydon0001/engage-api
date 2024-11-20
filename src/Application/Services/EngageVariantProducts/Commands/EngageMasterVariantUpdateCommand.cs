
namespace Engage.Application.Services.EngageVariantProducts.Commands;

public class EngageMasterVariantUpdateCommand : IRequest<EngageVariantProduct>, IMapTo<EngageMasterProduct>, IMapTo<EngageVariantProduct>
{
    public int Id { get; set; }
    public int SupplierId { get; set; }
    public int ProductClassificationId { get; set; }
    public int EngageDepartmentId { get; set; }
    public int EngageSubCategoryId { get; set; }
    public int EngageBrandId { get; set; }
    public int VatId { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public bool IsDairyProduct { get; set; }
    public bool IsVATProduct { get; set; }
    public bool Disabled { get; set; }
    public bool IsAllSuppliersProduct { get; set; }
    public bool IsFreshProduct { get; set; }
    public bool IsDropShipment { get; set; }

    public List<int> EngageTagIds { get; set; }

    public string EANNumber { get; set; }
    public int UnitTypeId { get; set; }
    public float Size { get; set; }
    public float PackSize { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EngageMasterVariantUpdateCommand, EngageMasterProduct>();

        profile.CreateMap<EngageMasterVariantUpdateCommand, EngageVariantProduct>();
    }
}

public record EngageMasterVariantUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<EngageMasterVariantUpdateCommand, EngageVariantProduct>
{
    public async Task<EngageVariantProduct> Handle(EngageMasterVariantUpdateCommand command, CancellationToken cancellationToken)
    {
        var master = await Context.EngageMasterProducts.FirstOrDefaultAsync(e => e.EngageMasterProductId == command.Id, cancellationToken)
            ?? throw new Exception("Master Product not found");

        var mappedMaster = Mapper.Map(command, master);

        var variant = await Context.EngageVariantProducts.FirstOrDefaultAsync(e => e.EngageMasterProductId == command.Id
                                            && e.IsMaster == true, cancellationToken)
                                                    ?? throw new Exception("No master variant found");

        var mappedVariant = Mapper.Map(command, variant);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedVariant;
    }
}

public class EngageMasterVariantUpdateValidator : AbstractValidator<EngageMasterVariantUpdateCommand>
{
    public EngageMasterVariantUpdateValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
        RuleFor(x => x.SupplierId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.ProductClassificationId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.EngageDepartmentId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.EngageSubCategoryId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.EngageBrandId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.VatId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.Code).MaximumLength(30).NotEmpty();
        RuleFor(x => x.Name).MaximumLength(220).NotEmpty();
        RuleForEach(x => x.EngageTagIds).GreaterThan(0);

        RuleFor(x => x.Size).GreaterThan(0).NotEmpty();
        RuleFor(x => x.PackSize).GreaterThan(0).NotEmpty();
        RuleFor(x => x.UnitTypeId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.EANNumber).MaximumLength(20);
    }
}