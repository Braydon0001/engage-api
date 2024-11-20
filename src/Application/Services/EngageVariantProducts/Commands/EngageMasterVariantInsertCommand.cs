

using Engage.Application.Services.Shared.AssignCommands;

namespace Engage.Application.Services.EngageVariantProducts.Commands;

public class EngageMasterVariantInsertCommand : IRequest<EngageVariantProduct>, IMapTo<EngageMasterProduct>
{
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
        profile.CreateMap<EngageMasterVariantInsertCommand, EngageMasterProduct>();
    }
}

public record EngageMasterVariantInsertHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator) : IRequestHandler<EngageMasterVariantInsertCommand, EngageVariantProduct>
{
    public async Task<EngageVariantProduct> Handle(EngageMasterVariantInsertCommand command, CancellationToken cancellationToken)
    {
        var master = Mapper.Map<EngageMasterVariantInsertCommand, EngageMasterProduct>(command);

        var masterCode = await Context.EngageMasterProducts
                                        .Where(e => e.Code == master.Code)
                                        .FirstOrDefaultAsync(cancellationToken);

        if (masterCode != null)
        {
            throw new Exception("A product with that code already exists");
        }

        EngageVariantProduct masterVariant = new()
        {
            Name = command.Name,
            Code = command.Code,
            EANNumber = command.EANNumber,
            UnitTypeId = command.UnitTypeId,
            Size = command.Size,
            PackSize = command.PackSize,
            IsMaster = true
        };

        Context.EngageMasterProducts.Add(master);

        await Context.SaveChangesAsync(cancellationToken);

        if (command.EngageTagIds != null && command.EngageTagIds.Any())
        {
            await Mediator.Send(new BatchAssignCommand(AssignDesc.TAG_PRODUCT, master.EngageMasterProductId, command.EngageTagIds), cancellationToken);
        }

        masterVariant.EngageMasterProductId = master.EngageMasterProductId;

        Context.EngageVariantProducts.Add(masterVariant);

        await Context.SaveChangesAsync(cancellationToken);

        return masterVariant;
    }
}

public class EngageMasterVariantInsertValidator : AbstractValidator<EngageMasterVariantInsertCommand>
{
    public EngageMasterVariantInsertValidator()
    {
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