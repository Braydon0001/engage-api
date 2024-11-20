// auto-generated
namespace Engage.Application.Services.ProductTransactions.Commands;

public class ProductTransactionUpdateCommand : IMapTo<ProductTransaction>, IRequest<ProductTransaction>
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int ProductTransactionTypeId { get; set; }
    public int ProductTransactionStatusId { get; set; }
    public int ProductPeriodId { get; set; }
    public int ProductWarehouseId { get; set; }
    public int? EmployeeId { get; set; }
    public float Quantity { get; set; }
    public decimal Price { get; set; }
    public DateTime TransactionDate { get; set; }
    public string Note { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductTransactionUpdateCommand, ProductTransaction>();
    }
}

public class ProductTransactionUpdateHandler : UpdateHandler, IRequestHandler<ProductTransactionUpdateCommand, ProductTransaction>
{
    public ProductTransactionUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProductTransaction> Handle(ProductTransactionUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.ProductTransactions.SingleOrDefaultAsync(e => e.ProductTransactionId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
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

        command.ProductPeriodId = transactionPeriod.ProductPeriodId;

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateProductTransactionValidator : AbstractValidator<ProductTransactionUpdateCommand>
{
    public UpdateProductTransactionValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProductId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProductTransactionTypeId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.EmployeeId);
        RuleFor(e => e.ProductPeriodId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProductWarehouseId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Quantity).NotEmpty();
        RuleFor(e => e.Price).NotEmpty();
        RuleFor(e => e.TransactionDate).NotEmpty();
        RuleFor(e => e.Note).MaximumLength(1000);
    }
}