// auto-generated
namespace Engage.Application.Services.ProductMasterSizes.Commands;

public class ProductMasterSizeInsertCommand : IMapTo<ProductMasterSize>, IRequest<ProductMasterSize>
{
    public int ProductMasterId { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductMasterSizeInsertCommand, ProductMasterSize>();
    }
}

public class ProductMasterSizeInsertHandler : InsertHandler, IRequestHandler<ProductMasterSizeInsertCommand, ProductMasterSize>
{
    public ProductMasterSizeInsertHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProductMasterSize> Handle(ProductMasterSizeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<ProductMasterSizeInsertCommand, ProductMasterSize>(command);
        
        _context.ProductMasterSizes.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class ProductMasterSizeInsertValidator : AbstractValidator<ProductMasterSizeInsertCommand>
{
    public ProductMasterSizeInsertValidator()
    {
        RuleFor(e => e.ProductMasterId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}