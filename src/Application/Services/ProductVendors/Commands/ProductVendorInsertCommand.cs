// auto-generated
namespace Engage.Application.Services.ProductVendors.Commands;

public class ProductVendorInsertCommand : IMapTo<ProductVendor>, IRequest<ProductVendor>
{
    public string Code { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductVendorInsertCommand, ProductVendor>();
    }
}

public class ProductVendorInsertHandler : InsertHandler, IRequestHandler<ProductVendorInsertCommand, ProductVendor>
{
    public ProductVendorInsertHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProductVendor> Handle(ProductVendorInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<ProductVendorInsertCommand, ProductVendor>(command);
        
        _context.ProductVendors.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class ProductVendorInsertValidator : AbstractValidator<ProductVendorInsertCommand>
{
    public ProductVendorInsertValidator()
    {
        RuleFor(e => e.Code).NotEmpty().MaximumLength(100);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}