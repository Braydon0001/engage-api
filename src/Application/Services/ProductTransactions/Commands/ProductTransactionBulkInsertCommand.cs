namespace Engage.Application.Services.ProductTransactions.Commands;

public class ProductTransactionBulkInsertCommand : IMapTo<ProductTransaction>, IRequest<List<ProductTransaction>>
{
    public int ProductWarehouseId { get; set; }
    public int ProductTransactionTypeId { get; set; }
    public int? ProductWarehouseInId { get; set; }
    public DateTime TransactionDate { get; set; }
    public int? EngageRegionId { get; set; }
    public int? EngageRegionInId { get; set; }
    public List<ProductTransactionBulkInsertProducts> ProductTransactions { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductTransactionBulkInsertCommand, ProductTransaction>()
            .ForMember(d => d.ProductWarehouseId, opt => opt.MapFrom(s => s.ProductWarehouseId)); ;
    }
}

public class ProductTransactionBulkInsertHandler : InsertHandler, IRequestHandler<ProductTransactionBulkInsertCommand, List<ProductTransaction>>
{
    private readonly IMediator _mediator;
    public ProductTransactionBulkInsertHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper)
    {
        _mediator = mediator;
    }

    public async Task<List<ProductTransaction>> Handle(ProductTransactionBulkInsertCommand command, CancellationToken cancellationToken)
    {
        if (command.ProductTransactions == null || command.ProductTransactions.Count <= 0)
        {
            throw new Exception("No transactions found");
        }

        var transactionPeriod = await _context.ProductPeriods
                                              .Where(s =>
                                                 command.TransactionDate >= s.StartDate
                                                 && command.TransactionDate <= s.EndDate)
                                              .FirstOrDefaultAsync(cancellationToken);

        if (transactionPeriod == null)
        {
            throw new Exception("Transaction Period not found");
        }

        var productTransaction = await _context.ProductTransactionStatuses
            .Where(e => e.ProductTransactionStatusId == 1)
            .FirstOrDefaultAsync(cancellationToken);

        if (productTransaction == null)
        {
            throw new Exception("No Product Transaction Status found");
        }

        List<ProductTransaction> result = new();

        foreach (var transaction in command.ProductTransactions)
        {
            var entity = await _mediator.Send(new ProductTransactionInsertCommand
            {
                EngageRegionId = command.EngageRegionId,
                EngageRegionInId = command.EngageRegionInId,
                ProductTransactionTypeId = command.ProductTransactionTypeId,
                Note = transaction.Note,
                Price = transaction.Price,
                ProductId = transaction.ProductId,
                ProductWarehouseId = command.ProductWarehouseId,
                ProductWarehouseInId = command.ProductWarehouseInId,
                Quantity = transaction.Quantity,
                TransactionDate = command.TransactionDate,
                SaveChanges = false
            }, cancellationToken);

            result.AddRange(entity);
        }

        await _context.SaveChangesAsync(cancellationToken);
        return result;
    }
}

public class ProductTransactionBulkInsertProducts
{
    public decimal Price { get; set; }
    public float Quantity { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public string Note { get; set; }
}