// auto-generated
namespace Engage.Application.Services.ProductBrands.Commands;

public class ProductBrandInsertCommand : IMapTo<ProductBrand>, IRequest<ProductBrand>
{
    public string Name { get; set; }
    public string SparBrand { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductBrandInsertCommand, ProductBrand>();
    }
}

public class ProductBrandInsertHandler : InsertHandler, IRequestHandler<ProductBrandInsertCommand, ProductBrand>
{
    public ProductBrandInsertHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProductBrand> Handle(ProductBrandInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<ProductBrandInsertCommand, ProductBrand>(command);
        
        _context.ProductBrands.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class ProductBrandInsertValidator : AbstractValidator<ProductBrandInsertCommand>
{
    public ProductBrandInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
        RuleFor(e => e.SparBrand).NotEmpty().MaximumLength(100);
    }
}