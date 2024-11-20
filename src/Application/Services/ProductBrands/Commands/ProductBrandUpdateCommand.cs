// auto-generated
namespace Engage.Application.Services.ProductBrands.Commands;

public class ProductBrandUpdateCommand : IMapTo<ProductBrand>, IRequest<ProductBrand>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string SparBrand { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductBrandUpdateCommand, ProductBrand>();
    }
}

public class ProductBrandUpdateHandler : UpdateHandler, IRequestHandler<ProductBrandUpdateCommand, ProductBrand>
{
    public ProductBrandUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProductBrand> Handle(ProductBrandUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.ProductBrands.SingleOrDefaultAsync(e => e.ProductBrandId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateProductBrandValidator : AbstractValidator<ProductBrandUpdateCommand>
{
    public UpdateProductBrandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
        RuleFor(e => e.SparBrand).NotEmpty().MaximumLength(100);
    }
}