// auto-generated
namespace Engage.Application.Services.InventoryTransactions.Commands;

public class InventoryTransactionUpdateCommand : IMapTo<InventoryTransaction>, IRequest<InventoryTransaction>
{
    public int Id { get; set; }
    public int InventoryTransactionTypeId { get; set; }
    public int InventoryTransactionStatusId { get; set; }
    public int InventoryId { get; set; }
    public int InventoryWarehouseId { get; set; }
    public float Quantity { get; set; }
    public DateTime TransactionDate { get; set; }
    public string Note { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<InventoryTransactionUpdateCommand, InventoryTransaction>();
    }
}

public class InventoryTransactionUpdateHandler : UpdateHandler, IRequestHandler<InventoryTransactionUpdateCommand, InventoryTransaction>
{
    public InventoryTransactionUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<InventoryTransaction> Handle(InventoryTransactionUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.InventoryTransactions.SingleOrDefaultAsync(e => e.InventoryTransactionId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateInventoryTransactionValidator : AbstractValidator<InventoryTransactionUpdateCommand>
{
    public UpdateInventoryTransactionValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.InventoryTransactionTypeId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.InventoryTransactionStatusId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.InventoryId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.InventoryWarehouseId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Quantity).NotEmpty();
        RuleFor(e => e.TransactionDate).NotEmpty();
        RuleFor(e => e.Note).MaximumLength(1000);
    }
}