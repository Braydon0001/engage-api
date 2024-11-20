// auto-generated
namespace Engage.Application.Services.SupplierContractAmounts.Commands;

public class SupplierContractAmountUpdateCommand : IMapTo<SupplierContractAmount>, IRequest<SupplierContractAmount>
{
    public int Id { get; set; }
    public int SupplierSubContractDetailId { get; set; }
    public int SupplierContractAmountTypeId { get; set; }
    public int? SupplierContractSplitId { get; set; }
    public float Amount { get; set; }
    public float? StartRangeAmount { get; set; }
    public float? EndRangeAmount { get; set; }
    public bool IsAmountPercent { get; set; }
    public bool IsRangeAmountPercent { get; set; }
    public List<JsonText> Note { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierContractAmountUpdateCommand, SupplierContractAmount>();
    }
}

public class SupplierContractAmountUpdateHandler : UpdateHandler, IRequestHandler<SupplierContractAmountUpdateCommand, SupplierContractAmount>
{
    public SupplierContractAmountUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<SupplierContractAmount> Handle(SupplierContractAmountUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.SupplierContractAmounts.SingleOrDefaultAsync(e => e.SupplierContractAmountId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        entity.Note = null;

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateSupplierContractAmountValidator : AbstractValidator<SupplierContractAmountUpdateCommand>
{
    public UpdateSupplierContractAmountValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.SupplierSubContractDetailId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.SupplierContractAmountTypeId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.SupplierContractSplitId);
        RuleFor(e => e.Amount).NotEmpty();
        RuleFor(e => e.StartRangeAmount);
        RuleFor(e => e.EndRangeAmount);
        RuleFor(e => e.IsAmountPercent);
        RuleFor(e => e.IsRangeAmountPercent);
    }
}