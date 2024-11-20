// auto-generated
namespace Engage.Application.Services.ProductSuppliers.Commands;

public class ProductSupplierInsertCommand : IMapTo<ProductSupplier>, IRequest<ProductSupplier>
{
    public string Code { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductSupplierInsertCommand, ProductSupplier>();
    }
}

public class ProductSupplierInsertHandler : InsertHandler, IRequestHandler<ProductSupplierInsertCommand, ProductSupplier>
{
    public ProductSupplierInsertHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProductSupplier> Handle(ProductSupplierInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<ProductSupplierInsertCommand, ProductSupplier>(command);
        
        _context.ProductSuppliers.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class ProductSupplierInsertValidator : AbstractValidator<ProductSupplierInsertCommand>
{
    public ProductSupplierInsertValidator()
    {
        RuleFor(e => e.Code).NotEmpty().MaximumLength(100);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}