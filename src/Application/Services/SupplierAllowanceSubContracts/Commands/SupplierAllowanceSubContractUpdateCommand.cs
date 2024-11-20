namespace Engage.Application.Services.SupplierAllowanceSubContracts.Commands;

public class SupplierAllowanceSubContractUpdateCommand : IMapTo<SupplierAllowanceSubContract>, IRequest<SupplierAllowanceSubContract>
{
    public int Id { get; set; }
    public int SupplierAllowanceContractId { get; set; }
    public string Category { get; set; }
    public string Name { get; set; }
    public string Vendor { get; set; }
    public string Comment { get; set; }
    public string Note { get; set; }
    public string GlSubCode { get; set; }
    public string GlMainCode { get; set; }
    public string NCircular { get; set; }
    public float WarehouseAllowancePercent { get; set; }
    public string WarehouseAllowanceNote { get; set; }
    public float RedistributionPercent { get; set; }
    public string RedistributionNote { get; set; }
    public float SwellPercent { get; set; }
    public string SwellNote { get; set; }
    public float RebatePercent { get; set; }
    public string RebateNote { get; set; }
    public float SettlementPercent { get; set; }
    public string SettlementNote { get; set; }
    public float EncoreHouseAllowancePercent { get; set; }
    public string EncoreHouseAllowanceNote { get; set; }
    public float EncoreTradeMarketingPercent { get; set; }
    public string EncoreTradeMarketingNote { get; set; }
    public float AdvertisingMarketingAllowancePercent { get; set; }
    public string AdvertisingMarketingAllowanceNote { get; set; }
    public float CatmanPercent { get; set; }
    public string CatmanNote { get; set; }
    public float EngagePercent { get; set; }
    public string EngageNote { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierAllowanceSubContractUpdateCommand, SupplierAllowanceSubContract>();
    }
}

public class SupplierAllowanceSubContractUpdateHandler : UpdateHandler, IRequestHandler<SupplierAllowanceSubContractUpdateCommand, SupplierAllowanceSubContract>
{
    public SupplierAllowanceSubContractUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<SupplierAllowanceSubContract> Handle(SupplierAllowanceSubContractUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.SupplierAllowanceSubContracts.SingleOrDefaultAsync(e => e.SupplierAllowanceSubContractId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateSupplierAllowanceSubContractValidator : AbstractValidator<SupplierAllowanceSubContractUpdateCommand>
{
    public UpdateSupplierAllowanceSubContractValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.SupplierAllowanceContractId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Category);
        RuleFor(e => e.Vendor).MaximumLength(100);
        RuleFor(e => e.NCircular).MaximumLength(100);
        RuleFor(e => e.WarehouseAllowancePercent);
        RuleFor(e => e.WarehouseAllowanceNote).MaximumLength(100);
        RuleFor(e => e.RedistributionPercent);
        RuleFor(e => e.RedistributionNote).MaximumLength(100);
        RuleFor(e => e.SwellPercent);
        RuleFor(e => e.SwellNote).MaximumLength(100);
        RuleFor(e => e.RebatePercent);
        RuleFor(e => e.RebateNote).MaximumLength(100);
        RuleFor(e => e.SettlementPercent);
        RuleFor(e => e.SettlementNote).MaximumLength(100);
        RuleFor(e => e.EncoreHouseAllowancePercent);
        RuleFor(e => e.EncoreHouseAllowanceNote).MaximumLength(100);
        RuleFor(e => e.EncoreTradeMarketingPercent);
        RuleFor(e => e.EncoreTradeMarketingNote).MaximumLength(100);
        RuleFor(e => e.AdvertisingMarketingAllowancePercent);
        RuleFor(e => e.AdvertisingMarketingAllowanceNote).MaximumLength(100);
        RuleFor(e => e.CatmanPercent);
        RuleFor(e => e.CatmanNote).MaximumLength(100);
        RuleFor(e => e.EngagePercent);
        RuleFor(e => e.EngageNote).MaximumLength(100);
        RuleFor(e => e.Comment);
        RuleFor(e => e.Note);
        RuleFor(e => e.GlSubCode).MaximumLength(100);
        RuleFor(e => e.GlMainCode).MaximumLength(100);
    }
}