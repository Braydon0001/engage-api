// auto-generated
namespace Engage.Application.Services.SupplierContractAmounts.Commands;

public class SupplierContractAmountInsertCommand : IMapTo<SupplierContractAmount>, IRequest<SupplierContractAmount>
{
    public int SupplierSubContractDetailId { get; set; }
    public int SupplierContractAmountTypeId { get; set; }
    //public int? SupplierContractSplitId { get; set; }
    public List<int> SupplierContractSplitIds { get; set; }
    public float Amount { get; set; }
    public float? StartRangeAmount { get; set; }
    public float? EndRangeAmount { get; set; }
    public bool IsAmountPercent { get; set; }
    public bool IsRangeAmountPercent { get; set; }
    public List<JsonText> Note { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierContractAmountInsertCommand, SupplierContractAmount>();
    }
}

public class SupplierContractAmountInsertHandler : InsertHandler, IRequestHandler<SupplierContractAmountInsertCommand, SupplierContractAmount>
{
    public SupplierContractAmountInsertHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<SupplierContractAmount> Handle(SupplierContractAmountInsertCommand command, CancellationToken cancellationToken)
    {
        SupplierContractAmount entity = null;

        if (command.SupplierContractAmountTypeId == 3 && command.SupplierContractSplitIds.Count > 0)
        {
            var splitAmount = (float)Math.Round(command.Amount / command.SupplierContractSplitIds.Count, 2);

            foreach (var splitId in command.SupplierContractSplitIds)
            {
                entity = _mapper.Map<SupplierContractAmountInsertCommand, SupplierContractAmount>(command);
                entity.Amount = splitAmount;
                entity.SupplierContractSplitId = splitId;

                _context.SupplierContractAmounts.Add(entity);
            }
        }
        else
        {
            entity = _mapper.Map<SupplierContractAmountInsertCommand, SupplierContractAmount>(command);

            _context.SupplierContractAmounts.Add(entity);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class SupplierContractAmountInsertValidator : AbstractValidator<SupplierContractAmountInsertCommand>
{
    public SupplierContractAmountInsertValidator()
    {
        RuleFor(e => e.SupplierSubContractDetailId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.SupplierContractAmountTypeId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.SupplierContractSplitIds);
        RuleFor(e => e.Amount).NotEmpty();
        RuleFor(e => e.StartRangeAmount);
        RuleFor(e => e.EndRangeAmount);
        RuleFor(e => e.IsAmountPercent);
        RuleFor(e => e.IsRangeAmountPercent);
    }
}