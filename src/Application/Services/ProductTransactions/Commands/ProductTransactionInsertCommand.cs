// auto-generated
namespace Engage.Application.Services.ProductTransactions.Commands;

public class ProductTransactionInsertCommand : IMapTo<ProductTransaction>, IRequest<List<ProductTransaction>>
{
    public int ProductId { get; set; }
    public int ProductTransactionTypeId { get; set; }
    public int ProductWarehouseId { get; set; } // transfer: WarehouseOut
    public int? ProductWarehouseInId { get; set; }
    public int ProductPeriodId { get; set; }
    public int? EmployeeId { get; set; }
    public float Quantity { get; set; }
    public decimal Price { get; set; }
    public DateTime TransactionDate { get; set; }
    public string Note { get; set; }
    public int? EngageRegionId { get; set; }
    public int? EngageRegionInId { get; set; }
    public bool SaveChanges { get; set; } = true;
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductTransactionInsertCommand, ProductTransaction>()
            .ForMember(d => d.ProductWarehouseId, opt => opt.MapFrom(s => s.ProductWarehouseId));
    }
}

public class ProductTransactionInsertHandler : InsertHandler, IRequestHandler<ProductTransactionInsertCommand, List<ProductTransaction>>
{
    public ProductTransactionInsertHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<ProductTransaction>> Handle(ProductTransactionInsertCommand command, CancellationToken cancellationToken)
    {

        var transactionPeriod = await _context.ProductPeriods
                                              .Where(s =>
                                                 command.TransactionDate >= s.StartDate
                                                 && command.TransactionDate <= s.EndDate)
                                              .FirstOrDefaultAsync(cancellationToken);
        if (transactionPeriod == null)
        {
            throw new Exception("Transaction Period not found");
        }

        command.ProductPeriodId = transactionPeriod.ProductPeriodId;

        var productTransaction = await _context.ProductTransactionStatuses
            .Where(e => e.ProductTransactionStatusId == 1)
            .FirstOrDefaultAsync(cancellationToken);

        if (productTransaction == null)
        {
            throw new Exception("No Product Transaction Status found");
        }

        List<ProductTransaction> entities = new();

        //using TransferIn as a reference to transfer transaction type
        if (command.ProductTransactionTypeId == (int)ProductTransactionTypeEnum.TransferIn)
        {
            if (!command.ProductWarehouseInId.HasValue)
            {
                throw new Exception("No transfer warehouse found");
            }
            var transferOut = new ProductTransaction()
            {
                ProductId = command.ProductId,
                ProductTransactionTypeId = (int)ProductTransactionTypeEnum.TransferOut,
                ProductTransactionStatus = productTransaction,
                ProductPeriodId = transactionPeriod.ProductPeriodId,
                ProductWarehouseId = command.ProductWarehouseId,
                Quantity = command.Quantity * -1,
                Price = command.Price,
                TransactionDate = command.TransactionDate,
                EngageRegionId = command.EngageRegionId,
                Note = command.Note,
            };
            var transferIn = new ProductTransaction()
            {
                ProductId = command.ProductId,
                ProductTransactionTypeId = (int)ProductTransactionTypeEnum.TransferIn,
                ProductTransactionStatus = productTransaction,
                ProductPeriodId = transactionPeriod.ProductPeriodId,
                ProductWarehouseId = command.ProductWarehouseInId.Value,
                Quantity = command.Quantity,
                Price = command.Price,
                TransactionDate = command.TransactionDate,
                EngageRegionId = command.EngageRegionInId,
                Note = command.Note,
            };
            _context.ProductTransactions.Add(transferOut);
            _context.ProductTransactions.Add(transferIn);

            entities.Add(transferOut);
            entities.Add(transferIn);

        }
        else if (command.ProductTransactionTypeId == (int)ProductTransactionTypeEnum.Issue)
        {
            var entity = _mapper.Map<ProductTransactionInsertCommand, ProductTransaction>(command);
            entity.ProductId = command.ProductId;

            entity.ProductTransactionStatusId = productTransaction.ProductTransactionStatusId;
            entity.Quantity *= -1;

            _context.ProductTransactions.Add(entity);

            entities.Add(entity);

        }
        else
        {
            var entity = _mapper.Map<ProductTransactionInsertCommand, ProductTransaction>(command);
            entity.ProductId = command.ProductId;

            entity.ProductTransactionStatusId = productTransaction.ProductTransactionStatusId;

            _context.ProductTransactions.Add(entity);

            entities.Add(entity);
        }
        if (command.SaveChanges)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        return entities;
    }
}

public class ProductTransactionInsertValidator : AbstractValidator<ProductTransactionInsertCommand>
{
    public ProductTransactionInsertValidator()
    {
        RuleFor(e => e.ProductId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProductTransactionTypeId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.EmployeeId);
        RuleFor(e => e.ProductPeriodId);
        RuleFor(e => e.EngageRegionId);
        RuleFor(e => e.ProductWarehouseId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProductWarehouseInId);
        RuleFor(e => e.Quantity).NotEmpty();
        RuleFor(e => e.Price);
        RuleFor(e => e.TransactionDate).NotEmpty();
        RuleFor(e => e.Note).MaximumLength(1000);
    }
}