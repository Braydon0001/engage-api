// auto-generated
namespace Engage.Application.Services.ProductTransactionStatuses.Commands;

public class ProductTransactionStatusInsertCommand : IMapTo<ProductTransactionStatus>, IRequest<ProductTransactionStatus>
{
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductTransactionStatusInsertCommand, ProductTransactionStatus>();
    }
}

public class ProductTransactionStatusInsertHandler : InsertHandler, IRequestHandler<ProductTransactionStatusInsertCommand, ProductTransactionStatus>
{
    public ProductTransactionStatusInsertHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProductTransactionStatus> Handle(ProductTransactionStatusInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<ProductTransactionStatusInsertCommand, ProductTransactionStatus>(command);
        
        _context.ProductTransactionStatuses.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class ProductTransactionStatusInsertValidator : AbstractValidator<ProductTransactionStatusInsertCommand>
{
    public ProductTransactionStatusInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}