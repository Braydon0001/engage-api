// auto-generated
namespace Engage.Application.Services.ProductMasterColors.Commands;

public class ProductMasterColorInsertCommand : IMapTo<ProductMasterColor>, IRequest<ProductMasterColor>
{
    public int ProductMasterId { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductMasterColorInsertCommand, ProductMasterColor>();
    }
}

public class ProductMasterColorInsertHandler : InsertHandler, IRequestHandler<ProductMasterColorInsertCommand, ProductMasterColor>
{
    public ProductMasterColorInsertHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProductMasterColor> Handle(ProductMasterColorInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<ProductMasterColorInsertCommand, ProductMasterColor>(command);

        _context.ProductMasterColors.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class ProductMasterColorInsertValidator : AbstractValidator<ProductMasterColorInsertCommand>
{
    public ProductMasterColorInsertValidator()
    {
        RuleFor(e => e.ProductMasterId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}