// auto-generated
namespace Engage.Application.Services.InventoryTransactions.Commands;

public class InventoryTransactionInsertCommand : IMapTo<InventoryTransaction>, IRequest<InventoryTransaction>
{
    public int InventoryTransactionTypeId { get; set; }
    public int InventoryTransactionStatusId { get; set; }
    public int InventoryId { get; set; }
    public int InventoryWarehouseId { get; set; }
    public float Quantity { get; set; }
    public DateTime TransactionDate { get; set; }
    public string Note { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<InventoryTransactionInsertCommand, InventoryTransaction>();
    }
}

public class InventoryTransactionInsertHandler : InsertHandler, IRequestHandler<InventoryTransactionInsertCommand, InventoryTransaction>
{
    public InventoryTransactionInsertHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<InventoryTransaction> Handle(InventoryTransactionInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<InventoryTransactionInsertCommand, InventoryTransaction>(command);
        
        _context.InventoryTransactions.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class InventoryTransactionInsertValidator : AbstractValidator<InventoryTransactionInsertCommand>
{
    public InventoryTransactionInsertValidator()
    {
        RuleFor(e => e.InventoryTransactionTypeId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.InventoryTransactionStatusId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.InventoryId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.InventoryWarehouseId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Quantity).NotEmpty();
        RuleFor(e => e.TransactionDate).NotEmpty();
        RuleFor(e => e.Note).MaximumLength(1000);
    }
}