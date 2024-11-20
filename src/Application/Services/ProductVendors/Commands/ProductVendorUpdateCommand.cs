// auto-generated
namespace Engage.Application.Services.ProductVendors.Commands;

public class ProductVendorUpdateCommand : IMapTo<ProductVendor>, IRequest<ProductVendor>
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductVendorUpdateCommand, ProductVendor>();
    }
}

public class ProductVendorUpdateHandler : UpdateHandler, IRequestHandler<ProductVendorUpdateCommand, ProductVendor>
{
    public ProductVendorUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProductVendor> Handle(ProductVendorUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.ProductVendors.SingleOrDefaultAsync(e => e.ProductVendorId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateProductVendorValidator : AbstractValidator<ProductVendorUpdateCommand>
{
    public UpdateProductVendorValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Code).NotEmpty().MaximumLength(100);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}