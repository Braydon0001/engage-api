// auto-generated
namespace Engage.Application.Services.SupplierBudgets.Commands;

public class SupplierBudgetInsertCommand : IMapTo<SupplierBudget>, IRequest<SupplierBudget>
{
    public int SupplierBudgetVersionId { get; set; }
    public int SupplierBudgetTypeId { get; set; }
    public int SupplierId { get; set; }
    public int? SupplierContractDetailId { get; set; }
    public int? EngageRegionId { get; set; }
    public float Amount { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierBudgetInsertCommand, SupplierBudget>();
    }
}

public class SupplierBudgetInsertHandler : InsertHandler, IRequestHandler<SupplierBudgetInsertCommand, SupplierBudget>
{
    public SupplierBudgetInsertHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<SupplierBudget> Handle(SupplierBudgetInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<SupplierBudgetInsertCommand, SupplierBudget>(command);
        
        _context.SupplierBudgets.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class SupplierBudgetInsertValidator : AbstractValidator<SupplierBudgetInsertCommand>
{
    public SupplierBudgetInsertValidator()
    {
        RuleFor(e => e.SupplierBudgetVersionId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.SupplierBudgetTypeId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.SupplierId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.SupplierContractDetailId);
        RuleFor(e => e.EngageRegionId);
        RuleFor(e => e.Amount).NotEmpty();
    }
}