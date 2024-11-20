namespace Engage.Application.Services.SparSubProducts.Commands;

public class SparSubProductInsertCommand : IMapTo<SparSubProduct>, IRequest<SparSubProduct>
{
    public int SparProductId { get; init; }
    public string DcCode { get; init; }
    public string Name { get; init; }
    public string Barcode { get; init; }
    public string CaseBarcode { get; init; }
    public string ShrinkBarcode { get; init; }
    public string PalletBarcode { get; init; }
    public bool IsPrimary { get; init; }
    public int SparSubProductStatusId { get; init; }
    public int? SparSourceId { get; init; }
    public int DistributionCenterId { get; init; }
    public string Warehouse { get; init; }
    public float? StockOnHand { get; init; }
    public float? StockOnOrder { get; init; }
    public string Note { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SparSubProductInsertCommand, SparSubProduct>();
    }
}

public record SparSubProductInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SparSubProductInsertCommand, SparSubProduct>
{
    public async Task<SparSubProduct> Handle(SparSubProductInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<SparSubProductInsertCommand, SparSubProduct>(command);

        Context.SparSubProducts.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class SparSubProductInsertValidator : AbstractValidator<SparSubProductInsertCommand>
{
    public SparSubProductInsertValidator()
    {
        RuleFor(e => e.SparProductId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.DcCode);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(200);
        RuleFor(e => e.Barcode).MaximumLength(30);
        RuleFor(e => e.CaseBarcode).MaximumLength(30);
        RuleFor(e => e.ShrinkBarcode).MaximumLength(30);
        RuleFor(e => e.PalletBarcode).MaximumLength(30);
        RuleFor(e => e.IsPrimary);
        RuleFor(e => e.SparSubProductStatusId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.SparSourceId);
        RuleFor(e => e.DistributionCenterId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Warehouse).MaximumLength(30);
        RuleFor(e => e.StockOnHand);
        RuleFor(e => e.StockOnOrder);
        RuleFor(e => e.Note).MaximumLength(220);
    }
}