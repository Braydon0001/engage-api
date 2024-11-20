// auto-generated
namespace Engage.Application.Services.ProductTransactionTypes.Commands;

public class ProductTransactionTypeInsertCommand : IMapTo<ProductTransactionType>, IRequest<ProductTransactionType>
{
    public string Name { get; set; }
    public bool IsPositive { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductTransactionTypeInsertCommand, ProductTransactionType>();
    }
}

public class ProductTransactionTypeInsertHandler : InsertHandler, IRequestHandler<ProductTransactionTypeInsertCommand, ProductTransactionType>
{
    public ProductTransactionTypeInsertHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProductTransactionType> Handle(ProductTransactionTypeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<ProductTransactionTypeInsertCommand, ProductTransactionType>(command);

        _context.ProductTransactionTypes.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class ProductTransactionTypeInsertValidator : AbstractValidator<ProductTransactionTypeInsertCommand>
{
    public ProductTransactionTypeInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
        RuleFor(e => e.IsPositive);
    }
}