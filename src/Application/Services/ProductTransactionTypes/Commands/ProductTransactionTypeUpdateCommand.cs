// auto-generated
namespace Engage.Application.Services.ProductTransactionTypes.Commands;

public class ProductTransactionTypeUpdateCommand : IMapTo<ProductTransactionType>, IRequest<ProductTransactionType>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsPositive { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductTransactionTypeUpdateCommand, ProductTransactionType>();
    }
}

public class ProductTransactionTypeUpdateHandler : UpdateHandler, IRequestHandler<ProductTransactionTypeUpdateCommand, ProductTransactionType>
{
    public ProductTransactionTypeUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProductTransactionType> Handle(ProductTransactionTypeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.ProductTransactionTypes.SingleOrDefaultAsync(e => e.ProductTransactionTypeId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateProductTransactionTypeValidator : AbstractValidator<ProductTransactionTypeUpdateCommand>
{
    public UpdateProductTransactionTypeValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
        RuleFor(e => e.IsPositive);
    }
}