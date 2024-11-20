// auto-generated
namespace Engage.Application.Services.ProductSuppliers.Commands;

public class ProductSupplierUpdateCommand : IMapTo<ProductSupplier>, IRequest<ProductSupplier>
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductSupplierUpdateCommand, ProductSupplier>();
    }
}

public class ProductSupplierUpdateHandler : UpdateHandler, IRequestHandler<ProductSupplierUpdateCommand, ProductSupplier>
{
    public ProductSupplierUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProductSupplier> Handle(ProductSupplierUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.ProductSuppliers.SingleOrDefaultAsync(e => e.ProductSupplierId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateProductSupplierValidator : AbstractValidator<ProductSupplierUpdateCommand>
{
    public UpdateProductSupplierValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Code).NotEmpty().MaximumLength(100);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}