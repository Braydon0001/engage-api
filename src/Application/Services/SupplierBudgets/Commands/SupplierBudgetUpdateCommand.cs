// auto-generated
namespace Engage.Application.Services.SupplierBudgets.Commands;

public class SupplierBudgetUpdateCommand : IMapTo<SupplierBudget>, IRequest<SupplierBudget>
{
    public int Id { get; set; }
    public int SupplierBudgetVersionId { get; set; }
    public int SupplierBudgetTypeId { get; set; }
    public int SupplierId { get; set; }
    public int? SupplierContractDetailId { get; set; }
    public int? EngageRegionId { get; set; }
    public float Amount { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierBudgetUpdateCommand, SupplierBudget>();
    }
}

public class SupplierBudgetUpdateHandler : UpdateHandler, IRequestHandler<SupplierBudgetUpdateCommand, SupplierBudget>
{
    public SupplierBudgetUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<SupplierBudget> Handle(SupplierBudgetUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.SupplierBudgets.SingleOrDefaultAsync(e => e.SupplierBudgetId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateSupplierBudgetValidator : AbstractValidator<SupplierBudgetUpdateCommand>
{
    public UpdateSupplierBudgetValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.SupplierBudgetVersionId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.SupplierBudgetTypeId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.SupplierId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.SupplierContractDetailId);
        RuleFor(e => e.EngageRegionId);
        RuleFor(e => e.Amount).NotEmpty();
    }
}